using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace FerreteriaNorte.DBConnection
{
    class DBConnection
    {
        public static string ConnectionString { get; set; }
        private static MySqlConnection SqlConnection;

        /*  MANNAGING THE CONNECTION OF DATABASE    */
        private static MySqlConnection GetConnection()
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
        private static void CloseConnection()
        {
            SqlConnection.Close();
            SqlConnection.Dispose();
        }

        /// <summary>
        /// Generate the Reader for a predefined string connection and passed sql query
        /// </summary>
        /// <param name="sqlQuery">The query for DB</param>
        /// <returns>Returns a Reader with the response of the DB</returns>
        public static MySqlDataReader ReadData(string sqlQuery)
        {
            MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, SqlConnection);
            return sqlCommand.ExecuteReader();
        }

        /*
         mySql.Open();
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM `cliente` WHERE 1", mySql);
                MySql.Data.MySqlClient.MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                string clients = "";
                while (mySqlDataReader.Read())
                {
                    clients += mySqlDataReader["nombre"];
                    clients += '\n';
                }

                MessageBox.Show(clients);
         */


    }
}
