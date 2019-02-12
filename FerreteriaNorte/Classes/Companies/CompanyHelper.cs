using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Companies
{
    class CompanyHelper
    {
        /// <summary>
        /// Parse a Company JSONObject to a Company class object
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Returns NULL if JSONObject don't contains id and name of Company</returns>
        public static Company parseCompany(string str)
        {
            JObject jsonCompany = JObject.Parse(str);
            string json_id = jsonCompany[DBKeys.Company.ID].ToString();

            int id = 0;
            string  name = jsonCompany[DBKeys.Company.NAME].ToString(),
                    web = jsonCompany[DBKeys.Company.WEB].ToString(),
                    cuit = jsonCompany[DBKeys.Company.CUIT].ToString();


            if (int.TryParse(json_id, out id) && name != null)
            {

                return new Company(id, name, web, cuit);
            }

            return null;
        }

        /// <summary>
        /// Call to DB API REST to get the Company list
        /// </summary>
        /// <returns>A list of Company objects</returns>
        public static List<Company> GetCompanys()
        {
            List<Company> companys = new List<Company>();

            string request = Properties.Resources.base_url + "company/list"; //Properties.Settings.Default.base_url + "/Company/list";

            string response = Functions.readRequest(request);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                Company newCompany = parseCompany(str);
                if (newCompany != null)
                {
                    companys.Add(newCompany);
                }
            }

            companys.Sort();

            return companys;
        }
    }
}
