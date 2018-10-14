using FerreteriaNorte.Classes;
using FerreteriaNorte.Resources.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FerreteriaNorte.DBConnection
{
    class ClientConnection
    {
        /// <summary>
        /// Returns the complete list of clients in the DB
        /// </summary>
        /// <returns></returns>
        public static List<Client> GetClients()
        {
            MySqlDataReader dataReader = DBConnection.ReadData("SELECT * FROM `cliente` WHERE 1");
            List<Client> clientList = new List<Client>();
            DateTimeFormatInfo dateTimeFormatInfo = CultureInfo.InvariantCulture.DateTimeFormat;

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

            return clientList;
        }


    }
}
