using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace FerreteriaNorte.Connection
{
    class DBConnection
    {
        public static string ConnectionString { get; set; }
        private static MySqlConnection SqlConnection = new MySqlConnection();

        /*  MANNAGING THE CONNECTION OF DATABASE    */
        protected static MySqlConnection GetConnection()
        {
            if (SqlConnection.State != System.Data.ConnectionState.Open && ConnectionString != "")
            {
                try
                {
                    SqlConnection.ConnectionString = ConnectionString;
                    SqlConnection.Open();
                }
                catch (Exception)
                {
                    CloseConnection();
                    throw;
                }
            }

            return SqlConnection;
        }

        /*  SIMPLIFIES THE DB CLOSE CONNECTION  */
        protected static void CloseConnection()
        {
            SqlConnection.Close();
            SqlConnection.Dispose();
        }

        /// <summary>
        /// Generate the Reader for a predefined string connection and passed sql query
        /// </summary>
        /// <param name="sqlQuery">The query for DB</param>
        /// <returns>Returns a Reader with the response of the DB</returns>
        protected static MySqlDataReader ReadData(string sqlQuery)
        {
            MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, SqlConnection);
            return sqlCommand.ExecuteReader();
        }

    }
}
