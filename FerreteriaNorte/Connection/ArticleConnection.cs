using FerreteriaNorte.Classes;
using FerreteriaNorte.Resources.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FerreteriaNorte.Connection
{
    class ArticleConnection : DBConnection
    {

        public static List<Article> GetArticles()
        {
            List<Article> ArticleList = new List<Article>();

            try
            {
                // start the connection to db
                DBConnection.GetConnection();
                MySqlDataReader dataReader = DBConnection.ReadData("SELECT * FROM `articulo` WHERE 1");

                // read values
                while (dataReader.Read())
                {
                    Article article = new Article();
                    article.Id = Convert.ToInt32(dataReader["id"]);
                    article.Descripcion = Convert.ToString(dataReader["descripcion"]);
                    article.Marca = Convert.ToString(dataReader["marca"]);
                    article.Foto = Functions.Base64ToImage(Convert.ToString(dataReader["foto"]));
                    article.Baja = Convert.ToBoolean(dataReader["baja"]);

                    ArticleList.Add(article);
                }

                // close the connection to DB
                DBConnection.CloseConnection();

            }
            catch (Exception)
            {
                DBConnection.CloseConnection();
                throw;
            }

            return ArticleList;
        }
    }
}
