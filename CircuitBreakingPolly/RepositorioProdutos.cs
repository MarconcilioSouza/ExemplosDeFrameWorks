using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace CircuitBreakingPolly
{
    public static class RepositorioProdutos
    {
        private static string connetionString = ConfigurationManager.ConnectionStrings["DBEntities"].ConnectionString;
        public static string sql = "SELECT * 77 FROM [Produtos].[dbo].[Produtos]";
        public static List<Produtos> GetProdutos()
        {
            List<Produtos> produtoList = new List<Produtos>();

            SqlConnection connection;
            SqlCommand command;

            // erro 
            SqlDataReader dataReader;
            connection = new SqlConnection(connetionString);
            connection.Open();
            command = new SqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Produtos produto = new Produtos();
                produto.ProdutoId = Convert.ToInt32(dataReader.GetValue(0));
                produto.Nome = dataReader.GetValue(1).ToString();
                produto.Decricao = dataReader.GetValue(2).ToString();
                produtoList.Add(produto);
            }
            dataReader.Close();
            command.Dispose();
            connection.Close();

            return produtoList;
        }
    }
}
