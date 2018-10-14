using FerreteriaNorte.Classes;
using FerreteriaNorte.Resources.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FerreteriaNorte.Connection
{
    class ClientConnection : DBConnection
    {
        /// <summary>
        /// Returns the complete list of clients in the DB
        /// </summary>
        /// <returns></returns>
        public static List<Client> GetClients()
        {
            List<Client> clientList = new List<Client>();

            
            try
            {
                // start the connection to db
                DBConnection.GetConnection();
                MySqlDataReader dataReader = DBConnection.ReadData("SELECT * FROM `cliente` WHERE 1");

                // read values
                while (dataReader.Read())
                {
                    Client client = new Client();
                    client.Id = Convert.ToInt32(dataReader["Id"]);
                    client.Documento = Convert.ToInt32(dataReader["Documento"]);
                    client.Nombre = Convert.ToString(dataReader["Nombre"]);
                    client.Direccion = Convert.ToString(dataReader["Direccion"]);
                    client.Provincia = Convert.ToString(dataReader["Provincia"]);
                    client.Pais = Convert.ToString(dataReader["Pais"]);
                    client.CodigoPostal = Convert.ToString(dataReader["CodigoPostal"]);
                    client.Condiciones = Convert.ToString(dataReader["Condiciones"]);
                    client.Celular = Convert.ToString(dataReader["Celular"]);
                    client.Telefono = Convert.ToString(dataReader["Telefono"]);
                    client.Email = Convert.ToString(dataReader["Email"]);
                    client.Limite = Convert.ToString(dataReader["Limite"]);
                    client.Lista = Convert.ToString(dataReader["Lista"]);
                    client.Foto = Functions.Base64ToImage(Convert.ToString(dataReader["Foto"]));
                    client.FechaInicio = Convert.ToDateTime(dataReader["FechaInicio"]);

                    clientList.Add(client);
                }

                // close the connection to DB
                DBConnection.CloseConnection();

            } catch (Exception err)
            {
                DBConnection.CloseConnection();
                throw;
            }

            return clientList;
        }


    }
}
