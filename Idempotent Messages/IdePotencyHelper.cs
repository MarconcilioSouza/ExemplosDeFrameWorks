using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Idempotent_Messages
{
    public static class IdePotencyHelper
    {
        public static bool ExecuteQuery(String commandText)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBEntities"].ConnectionString;
            bool retorno = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                try
                {
                    connection.Open();
                    var rowsAffected = (int)command.ExecuteScalar();
                    if (rowsAffected > 0)
                        retorno = true;
                }
                catch (Exception ex)
                {
                }
            }
            return retorno;
        }

        public static bool Execute<T1>(T1 obj)
        {
            var t = obj.GetType();
            int id = 0;
            string coluna = string.Empty;

            foreach (var prop in t.GetProperties())
            {
                if (prop.Name.Contains("Id"))
                {
                    id = (int)prop.GetValue(obj, null);
                    coluna = prop.Name;

                    break;
                }

            }

            string connectionString = ConfigurationManager.ConnectionStrings["DBEntities"].ConnectionString;
            string commandText = "SELECT count(1) FROM " + t.Name + " where " + coluna + " = @ID";
            bool retorno = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@ID", SqlDbType.Int);

                command.Parameters["@ID"].Value = id;

                try
                {
                    connection.Open();
                    var rowsAffected = (int)command.ExecuteScalar();
                    if (rowsAffected > 0)
                        retorno = true;
                }
                catch (Exception ex)
                {
                }
            }
            return retorno;
        }

        public static T ExecuteSave<T>(T args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBEntities"].ConnectionString;
            StringBuilder commandText = new StringBuilder("Insert into ");

            var TipoArgs = args.GetType();

            commandText.Append(TipoArgs.Name);
            string coluna = " (";
            string values = "";

            bool ok = true;
            foreach (var prop in TipoArgs.GetProperties())
            {
                if (prop.Name.Contains("Id"))
                    continue;

                if (ok)
                {
                    coluna += prop.Name;
                    values += "'" + prop.GetValue(args, null).ToString();
                }
                else
                {
                    coluna += " , " + prop.Name;
                    values += "' , '" + prop.GetValue(args, null).ToString();
                }
                ok = false;
            }

            coluna += ") Values (";
            values += "'); SELECT SCOPE_IDENTITY();";
            commandText.Append(coluna);
            commandText.Append(values);

            int idValue = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText.ToString(), connection);
                try
                {
                    connection.Open();
                    var id = command.ExecuteScalar().ToString();

                    Int32.TryParse(id, out idValue);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    connection.Close();
                }
            }
            foreach (var prop in TipoArgs.GetProperties())
            {
                if (prop.Name.Contains("Id"))
                {
                    prop.SetValue(args, idValue);
                    break;
                }
            }

            return args;
        }
    }
}
