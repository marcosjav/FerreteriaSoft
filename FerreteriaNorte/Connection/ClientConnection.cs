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

            } catch (Exception)
            {
                DBConnection.CloseConnection();
                throw;
            }

            return clientList;
        }

        public static bool AddClient(Client client)
        {
            try
            {
                // start the connection to db
                MySqlCommand sqlCommand = DBConnection.GetConnection().CreateCommand();

                sqlCommand.CommandText = "INSERT INTO `cliente`(`Nombre`, `Documento`, `Direccion`, `Provincia`, `Pais`, `CodigoPostal`, `Condiciones`, `Celular`, `Telefono`, `Email`, `FechaInicio`, `Limite`, `Lista`, `Foto`) VALUES (@Nombre, @Documento, @Direccion, @Provincia, @Pais, @CodigoPostal, @Condiciones, @Celular, @Telefono, @Email, @FechaInicio, @Limite, @Lista, @Foto)";

                sqlCommand.Parameters.AddWithValue("@Nombre", client.Nombre);
                sqlCommand.Parameters.AddWithValue("@Documento", client.Documento);
                sqlCommand.Parameters.AddWithValue("@Direccion", client.Direccion);
                sqlCommand.Parameters.AddWithValue("@Provincia", client.Provincia);
                sqlCommand.Parameters.AddWithValue("@Pais", client.Pais);
                sqlCommand.Parameters.AddWithValue("@CodigoPostal", client.CodigoPostal);
                sqlCommand.Parameters.AddWithValue("@Condiciones", client.Condiciones);
                sqlCommand.Parameters.AddWithValue("@Celular", client.Celular);
                sqlCommand.Parameters.AddWithValue("@Telefono", client.Telefono);
                sqlCommand.Parameters.AddWithValue("@Email", client.Email);
                sqlCommand.Parameters.AddWithValue("@FechaInicio", client.FechaInicio);
                sqlCommand.Parameters.AddWithValue("@Limite", client.Limite);
                sqlCommand.Parameters.AddWithValue("@Lista", client.Lista);
                sqlCommand.Parameters.AddWithValue("@Foto", Functions.ImageToBase64(client.Foto, System.Drawing.Imaging.ImageFormat.Jpeg));

                sqlCommand.ExecuteNonQuery();

                DBConnection.CloseConnection();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
