using FastMember;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.DataTable
{
    /// <summary>
    /// Extensions para funcionalidades adicionais para DbConnection.
    /// </summary>   
    public static class DbConnectionExtensions
    {
        #region [Properties]


        #endregion

        /// <summary>
        /// Realiza o bulk insert de uma lista de dados.
        /// </summary>
        /// <typeparam name="T">Tipo de dados da lista.</typeparam>
        /// <param name="conn">Objeto de conexão com a base de dados.</param>
        /// <param name="dataList">Lista de dados a ser enviado via Bulk Insert.</param>
        /// <param name="destinationTableName">Nome da tabela de destino.</param>
        public static void BulkInsert<T>(this IDbConnection conn,
            IList<T> dataList,
            string destinationTableName,
            Func<long, int> sqlRowsCopied = null)
        {
            using (var sqlDestination = new SqlConnection(conn.ConnectionString))
            {
                if (sqlDestination.State != ConnectionState.Open)
                    sqlDestination.Open();

                using (var sqlBulkCopy = new SqlBulkCopy(sqlDestination))
                {
                    sqlBulkCopy.BatchSize = dataList.Count;
                    sqlBulkCopy.DestinationTableName = destinationTableName;
                    var dataTable = GetDataTable(dataList);
                    sqlBulkCopy.WriteToServer(dataTable);
                }
            }

        }

        /// <summary>
        /// Retorna um datatable a partir de uma lista de dados genérica
        /// </summary>
        /// <typeparam name="T">Tipo de dados da lista.</typeparam>
        /// <param name="dataList">Lista de dados.</param>
        /// <returns>Datatable.</returns>
        public static System.Data.DataTable GetDataTable<T>(this IEnumerable<T> dataList)
        {
            var dataTable = new System.Data.DataTable();
            using (var reader = ObjectReader.Create(dataList))
            {
                dataTable.Load(reader);
            }

            return dataTable;
        }

        /// <summary>
        /// Retorna um datatable com base em uma lista de dados.
        /// </summary>
        /// <typeparam name="T">Tipo de dados que será transformado em Data Table.</typeparam>
        /// <param name="sqlBulkCopy">Bulk Copy do Sql Server.</param>
        /// <param name="dataList">Lista de dados que deverá ser enviada via bulk.</param>
        /// <returns>DataTable.</returns>
        private static System.Data.DataTable GetDataTableFromList<T>(this SqlBulkCopy sqlBulkCopy, IList<T> dataList)
        {
            var table = new System.Data.DataTable();
            var props = TypeDescriptor.GetProperties(typeof(T)).Cast<PropertyDescriptor>()
                .Where(propertyInfo => propertyInfo.PropertyType.Namespace != null && propertyInfo.PropertyType.Namespace.Equals("System")).ToArray();
            foreach (var propertyInfo in props)
            {
                sqlBulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
            }

            var values = new object[props.Length];
            foreach (var item in dataList)
            {
                for (var i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item);

                table.Rows.Add(values);
            }

            return table;
        }

    }
}
