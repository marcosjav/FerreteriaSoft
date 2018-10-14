using FerreteriaNorte.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Connection
{
    class AuthorizedConnection : DBConnection
    {
        public static List<Authorized> GetArticles()
        {
            List<Authorized> authorizedList = new List<Authorized>();

            try
            {
                // start the connection to db
                DBConnection.GetConnection();
                MySqlDataReader dataReader = DBConnection.ReadData("SELECT * FROM `autorizado` WHERE 1");

                // read values
                while (dataReader.Read())
                {
                    Authorized autorized = new Authorized();
                    autorized.IdAutorizado = Convert.ToInt32(dataReader["idAutorizado"]);
                    autorized.IdCliente = Convert.ToInt32(dataReader["IdCliente"]);
                    autorized.Nombre = Convert.ToString(dataReader["Nombre"]);
                    autorized.Documento = Convert.ToString(dataReader["Documento"]);

                    authorizedList.Add(autorized);
                }

                // close the connection to DB
                DBConnection.CloseConnection();

            }
            catch (Exception)
            {
                DBConnection.CloseConnection();
                throw;
            }

            return authorizedList;
        }
    }
}
