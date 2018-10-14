using FerreteriaNorte.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Connection
{
    class ProviderConnection : DBConnection
    {
        /// <summary>
        /// Returns the complete list of providers in the DB
        /// </summary>
        /// <returns></returns>
        public static List<Provider> GetProviders()
        {
            List<Provider> providerList = new List<Provider>();

            try
            {
                // start the connection to db
                DBConnection.GetConnection();
                MySqlDataReader dataReader = DBConnection.ReadData("SELECT * FROM `proveedor` WHERE 1");

                // read values
                while (dataReader.Read())
                {
                    Provider provider = new Provider();
                    provider.Id = Convert.ToInt32(dataReader["id"]);
                    provider.CUIT = Convert.ToInt32(dataReader["cuit"]);
                    provider.IdRubro = Convert.ToInt32(dataReader["id_rubro"]);
                    provider.IdCondicionVenta = Convert.ToInt32(dataReader["id_condicionventa"]);
                    provider.RazonSocial = Convert.ToString(dataReader["razonsocial"]);
                    provider.Direccion = Convert.ToString(dataReader["direccion"]);
                    provider.Provincia = Convert.ToString(dataReader["provincia"]);
                    provider.Pais = Convert.ToString(dataReader["pais"]);
                    provider.CodigoPostal = Convert.ToString(dataReader["codigopostal"]);
                    provider.Celular = Convert.ToString(dataReader["celular"]);
                    provider.Telefono = Convert.ToString(dataReader["telefono"]);
                    provider.Email = Convert.ToString(dataReader["email"]);
                    provider.IVA = Convert.ToString(dataReader["iva"]);
                    provider.Web = Convert.ToString(dataReader["web"]);
                    provider.FechaInicio =  Convert.ToDateTime(dataReader["fechainicio"]);

                    providerList.Add(provider);
                }

                // close the connection to DB
                DBConnection.CloseConnection();

            }
            catch (Exception)
            {
                DBConnection.CloseConnection();
                throw;
            }

            return providerList;
        }
    }
}
