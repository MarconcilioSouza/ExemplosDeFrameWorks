using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NHSecondLevelCache.Code
{
    public static class DbHelper
    {
        private static string connetionString = ConfigurationManager.ConnectionStrings["DBEntities"].ConnectionString;
        private static SqlConnectionStringBuilder _connStringBuilder = new SqlConnectionStringBuilder(connetionString);

        public static void ExecuteSql(string sql)
        {
            using (var conn = new SqlConnection(_connStringBuilder.ConnectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
