using FerreteriaNorte.Classes.Extra;
using FerreteriaNorte.Classes.Locations;
using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
                    web = jsonCompany[DBKeys.Company.WEB].ToString()?? "",
                    cuit = jsonCompany[DBKeys.Company.CUIT].ToString()?? "";


            if (int.TryParse(json_id, out id) && name != null)
            {
                List<Phone> phones = new List<Phone>();
                Address address = new Address();
                List<string> emails = new List<string>();

                if (jsonCompany["phones"] != null)
                {
                    JArray jPhones = new JArray(jsonCompany["phones"]);
                    
                    foreach (JObject item in jPhones)
                    {
                        jPhones.Add(PhoneHelper.parsePhone(item.ToString()));
                    }
                }

                if (jsonCompany["emails"] != null)
                {
                    JArray jEmails = new JArray(jsonCompany["emails"]);

                    foreach (string item in jEmails)
                    {
                        emails.Add(item);
                    }
                }
                return new Company(id, name, web, cuit, address, phones, emails);
            }

            return null;
        }

        /// <summary>
        /// Call to DB API REST to get the Company list
        /// </summary>
        /// <returns>A list of Company objects</returns>
        public static List<Company> GetCompanys(int id = 0)
        {
            List<Company> companys = new List<Company>();

            string request = Properties.Resources.base_url + "company/list"; //Properties.Settings.Default.base_url + "/Company/list";

            if (id > 0)
            {
                request += "?id_company=" + id;
            }
            string response = Functions.readRequest(request);

            if (id > 0)
            {
                JObject resp = JObject.Parse(response);
                Company company = parseCompany(resp.GetValue("0").ToString());
                List<Phone> phones = new List<Phone>();
                List<string> emails = new List<string>();

                foreach (JObject item in resp.GetValue("phones"))
                {
                    phones.Add(PhoneHelper.parsePhone(item.ToString()));
                }

                foreach (JObject item in resp.GetValue("emails"))
                {
                    emails.Add(item.GetValue(DBKeys.CompanyHasEmail.ADDRESS).ToString());
                }

                company.phones = phones;
                company.emails = emails;
                companys.Add(company);
            }
            else
            {
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
            }
            return companys;
        }

        public static Company GetCompany(int id)
        {
            List<Company> companys = new List<Company>();

            companys = GetCompanys(id);

            return companys[0]?? new Company();
        }

        public static void setCompanyGrid(DataGrid datagrid)
        {
            List<Company> companies = GetCompanys();
            datagrid.ItemsSource = companies;

            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            datagrid.IsReadOnly = true;

            foreach (DataGridColumn item in datagrid.Columns)
            {
                switch ((string)item.Header)
                {
                    case "Value":
                        item.Header = "Código";
                        item.DisplayIndex = 0;
                        break;
                    case "Text":
                        item.Header = "Nombre";
                        item.DisplayIndex = 1;
                        break;
                    //default:
                    //    item.Visibility = System.Windows.Visibility.Hidden;
                    //    break;
                }
            }
        }

        public static List<Company> LoadCompanyGrid(DataGrid datagrid, List<Company> companies = null)
        {
            if (companies == null)
                companies = GetCompanys();

            DataTable dt = new DataTable();

            CommonViewConfigurations.ConfigDataGrid(datagrid);

            // first add your columns
            dt.Columns.Add("#");
            dt.Columns.Add("Empresa");
            dt.Columns.Add("Web");

            foreach (Company company in companies)
            {
                var row = dt.NewRow();
                // Set values for columns with row[i] = xy
                row[0] = company.id.ToString("D5");
                row[1] = company.name;
                row[2] = company.web;

                dt.Rows.Add(row);
            }

            datagrid.DataContext = dt;
            return companies;
        }

        public static Tuple<HttpStatusCode, string> sendToDB(Company company, List<Address> addresses, List<Phone> phones, List<StringValue> emails)
        {
            JObject jCompany = new JObject();
            
            jCompany.Add(DBKeys.Company.NAME, company.name);
            jCompany.Add(DBKeys.Company.CUIT, company.cuit);
            jCompany.Add(DBKeys.Company.WEB, company.web);

            // ADDRESSES SECTION
            JArray jAddresses = new JArray();

            foreach (Address item in addresses)
            {
                JObject jAddress = new JObject();
                jAddress.Add(DBKeys.Address.STREET, item.street);
                jAddress.Add(DBKeys.Address.NUMBER, item.number);
                jAddress.Add(DBKeys.Address.ZIPCODE, item.zipCode);
                jAddress.Add(DBKeys.Address.CITY, item.city);
                jAddress.Add(DBKeys.Address.COORDINATES, item.coordinates);
                jAddresses.Add(jAddress);
            }

            jCompany.Add("addresses", jAddresses);

            // PHONES SECTION
            JArray jPhones = new JArray();

            foreach (Phone phone in phones)
            {
                JObject jPhone = new JObject();

                jPhone.Add(DBKeys.Phone.AREA, phone.area);
                jPhone.Add(DBKeys.Phone.NUMBER, phone.number);
                jPhone.Add(DBKeys.Phone.TYPE, phone.type);

                jPhones.Add(jPhone);
            }

            jCompany.Add("phones", jPhones);

            //  EMAILS SECTION
            JArray jEmails = new JArray();

            foreach (StringValue email in emails)
            {
                jEmails.Add(email.Value);
            }

            jCompany.Add("emails", jEmails);

            Tuple<HttpStatusCode, string> response = Functions.sendPost(Properties.Resources.base_url + "company/add", jCompany.ToString());

            return response;
        }
    }
}
