using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FerreteriaNorte.Classes.Locations
{
    class AddressHelper
    {
        private static List<Address> addresses;
        private static List<City> cities;
        private static List<Province> provinces;
        private static List<Country> countries;

        /*  PARSE SECTION   */

            /// <summary>
            /// Parse a Country JSONObject to a Country class object
            /// </summary>
            /// <param name="str"></param>
            /// <returns>Returns NULL if JSONObject don't contains id and name of Country</returns>
            public static Country parseCountry(string str)
            {
                JObject jsonCountry = JObject.Parse(str);

                int id = 0;
                string name = jsonCountry[DBKeys.Country.NAME].ToString();
                string json_id = jsonCountry[DBKeys.Country.ID].ToString();

                if (int.TryParse(json_id, out id) && name != null)
                {
                    return new Country(id, name);
                }

                return null;
            }

            /// <summary>
            /// Parse a Province JSONObject to a Province class object
            /// </summary>
            /// <param name="str"></param>
            /// <returns>Returns NULL if JSONObject don't contains a Province object</returns>
            public static Province parseProvince(string str)
            {
                JObject jsonProvince = JObject.Parse(str);

                int id = 0, country = 0;

                string json_id = jsonProvince[DBKeys.Province.ID].ToString();
                string name = jsonProvince[DBKeys.Province.NAME].ToString();
                string json_country = jsonProvince[DBKeys.Province.COUNTRY].ToString();

                if (int.TryParse(json_id, out id) && name != null && int.TryParse(json_country, out country))
                {
                    return new Province(id, name, country);
                }

                return null;
            }

            /// <summary>
            /// Parse a City JSONObject to a City class object
            /// </summary>
            /// <param name="str"></param>
            /// <returns>Returns NULL if JSONObject don't contains a City object</returns>
            public static City parseCity(string str)
            {
                JObject jsonCity = JObject.Parse(str);

                int id = 0, province = 0;

                string json_id = jsonCity[DBKeys.City.ID].ToString();
                string name = jsonCity[DBKeys.City.NAME].ToString();
                string json_province = jsonCity[DBKeys.City.PROVINCE].ToString();

                if (int.TryParse(json_id, out id) && name != null && int.TryParse(json_province, out province))
                {
                    return new City(id, name, province);
                }

                return null;
            }

            public static Address parseAddress(string str)
            {
                JObject jsonAddress = JObject.Parse(str);

                int id = 0, city = 0;

                string street = jsonAddress[DBKeys.Address.STREET].ToString();
                string number = jsonAddress[DBKeys.Address.NUMBER].ToString()?? "0";
                string zip_code = jsonAddress[DBKeys.Address.ZIPCODE].ToString()?? "0";
                string json_id = jsonAddress[DBKeys.Address.ID].ToString();
                string json_city= jsonAddress[DBKeys.Address.CITY].ToString();
                string coordinates = jsonAddress[DBKeys.Address.COORDINATES].ToString() ?? "";

                if (int.TryParse(json_id, out id) && street != null && int.TryParse(json_city, out city))
                {
                    return new Address(id, street, number,zip_code, coordinates, city);
                }

                return null;
            }

        /*  END - PARSE SECTION */

        /*  API REST SECTION  */

        /// <summary>
        /// Call to DB API REST to get the Country list
        /// </summary>
        /// <returns>A list of Country objects</returns>
        public static List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();

            string request = Properties.Resources.base_url + "location/country";

            string response = Functions.readRequest(request);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                Country newCountry = parseCountry(str);
                if (newCountry != null)
                {
                    countries.Add(newCountry);
                }
            }

            countries.Sort();

            return countries;
        }

        /// <summary>
        /// Call to DB API REST to get the Province list
        /// </summary>
        /// <returns>A list of Province objects</returns>
        public static List<Province> GetProvinces(int country_id = 0)
        {
            List<Province> provinces = new List<Province>();

            //string request = Properties.Resources.base_url + "location/province";

            //string response = Functions.readRequest(request);

            Dictionary<string, string> parms = new Dictionary<string, string>();
            if (country_id > 0)
                parms.Add(DBKeys.Country.ID, country_id.ToString());

            string response = Functions.createRequest("location/province", parms);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                Province newProvince = parseProvince(str);
                if (newProvince != null)
                {
                    provinces.Add(newProvince);
                }
            }

            provinces.Sort();

            return provinces;
        }

        /// <summary>
        /// Call to DB API REST to get the City list
        /// </summary>
        /// <returns>A list of City objects</returns>
        public static List<City> GetCities(int province_id = 0, int country_id = 0)
        {
            List<City> cities = new List<City>();

            //string request = Properties.Resources.base_url + "location/city";

            //string response = Functions.readRequest(request);

            Dictionary<string, string> parms = new Dictionary<string, string>();

            if (province_id > 0)
                parms.Add(DBKeys.Province.ID, province_id.ToString());

            if (country_id > 0)
                parms.Add(DBKeys.Country.ID, country_id.ToString());

            string response;
            if (country_id > 0)
                response = Functions.createRequest("location/list", parms);
            else
                response = Functions.createRequest("location/city", parms);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                City newCity = parseCity(str);
                if (newCity != null)
                {
                    cities.Add(newCity);
                }
            }

            cities.Sort();

            return cities;
        }

        /// <summary>
        /// Call to DB API REST to get the Address list
        /// </summary>
        /// <returns>A list of Address objects</returns>
        public static List<Address> GetAddresses()
        {
            List<Address> addresses = new List<Address>();

            string request = Properties.Resources.base_url + "address/address";

            string response = Functions.readRequest(request);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                Address newAddress = parseAddress(str);
                if (newAddress != null)
                {
                    addresses.Add(newAddress);
                }
            }

            addresses.Sort();

            return addresses;
        }

        /*  END - API REST SECTION  */

        /* DATAGRID FUNCTIONS  */
        public static void setCountryGrid(DataGrid datagrid)
        {
            List<Country> items = GetCountries();

            datagrid.ItemsSource = items;

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
                        item.Header = "País";
                        item.DisplayIndex = 1;
                        break;
                    default:
                        item.Visibility = System.Windows.Visibility.Hidden;
                        break;
                }
            }

        }

        public static void setProvinceGrid(DataGrid datagrid, int country_id = 0)
        {
            List<Province> items = GetProvinces(country_id);

            datagrid.ItemsSource = items;

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
                        item.Header = "Provincia";
                        item.DisplayIndex = 1;
                        break;
                    default:
                        item.Visibility = System.Windows.Visibility.Hidden;
                        break;
                }
            }

        }

        public static void setCityGrid(DataGrid datagrid, int province_id = 0, int country_id = 0)
        {
            List<City> items = GetCities(province_id, country_id);

            datagrid.ItemsSource = items;

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
                        item.Header = "Ciudad";
                        item.DisplayIndex = 1;
                        break;
                    default:
                        item.Visibility = System.Windows.Visibility.Hidden;
                        break;
                }
            }

        }

        public static void setAddressGrid(DataGrid datagrid, List<Address> addresses = null)
        {
            List<Address> items = addresses;//?? GetAddress();
            
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = items;

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
                        item.Header = "Dirección";
                        item.DisplayIndex = 1;
                        break;
                    default:
                        item.Visibility = System.Windows.Visibility.Hidden;
                        break;
                }
            }

        }

        public static void LoadCountryGrid(DataGrid datagrid)
        {
            List<Country> brands = GetCountries();
            DataTable dt = new DataTable();

            CommonViewConfigurations.ConfigDataGrid(datagrid);

            // first add your columns
            dt.Columns.Add("Código");
            dt.Columns.Add("País");

            foreach (Country item in brands)
            {
                var row = dt.NewRow();
                // Set values for columns with row[i] = xy
                row[0] = item.id.ToString("D5");
                row[1] = item.name;

                dt.Rows.Add(row);
            }

            datagrid.DataContext = dt;
        }

        public static void LoadProvinceGrid(DataGrid datagrid)
        {
            List<Province> brands = GetProvinces();
            DataTable dt = new DataTable();

            CommonViewConfigurations.ConfigDataGrid(datagrid);

            // first add your columns
            dt.Columns.Add("Código");
            dt.Columns.Add("Provincia");

            foreach (Province item in brands)
            {
                var row = dt.NewRow();
                // Set values for columns with row[i] = xy
                row[0] = item.id.ToString("D5");
                row[1] = item.name;

                dt.Rows.Add(row);
            }

            datagrid.DataContext = dt;
        }

        public static void LoadCityGrid(DataGrid datagrid)
        {
            List<City> brands = GetCities();
            DataTable dt = new DataTable();

            CommonViewConfigurations.ConfigDataGrid(datagrid);

            // first add your columns
            dt.Columns.Add("Código");
            dt.Columns.Add("Ciudad");

            foreach (City item in brands)
            {
                var row = dt.NewRow();
                // Set values for columns with row[i] = xy
                row[0] = item.id.ToString("D5");
                row[1] = item.name;

                dt.Rows.Add(row);
            }

            datagrid.DataContext = dt;
        }

        public static void LoadAddressGrid(DataGrid datagrid)
        {
            List<Address> brands = GetAddresses();
            DataTable dt = new DataTable();

            CommonViewConfigurations.ConfigDataGrid(datagrid);

            // first add your columns
            dt.Columns.Add("Código");
            dt.Columns.Add("Calle");
            dt.Columns.Add("Número");
            dt.Columns.Add("CP");
            //dt.Columns.Add("Coordenadas");

            foreach (Address item in brands)
            {
                var row = dt.NewRow();
                // Set values for columns with row[i] = xy
                row[0] = item.id.ToString("D5");
                row[1] = item.street;
                row[2] = item.number;
                row[3] = item.zipCode;

                dt.Rows.Add(row);
            }

            datagrid.DataContext = dt;
        }

        /* END DATAGRID FUNCTIONS  */

        /* DATABASE FUNCTIONS */

        public static int sendCountryToDB(Country country)
        {
            string request = Properties.Resources.base_url + "location/add";

            request += "?" + DBKeys.Country.NAME + "=" + country.name;

            string response = Functions.readRequest(request);

            if (response != null)
            {
                return int.Parse(response);
            }

            return 0;
        }

        public static int sendProvinceToDB(Province province)
        {
            string request = Properties.Resources.base_url + "location/add";

            request += "?" + DBKeys.Province.NAME + "=" + province.name;
            request += "&" + DBKeys.Province.COUNTRY + "=" + province.country;

            string response = Functions.readRequest(request);

            if (response != null)
            {
                return int.Parse(response);
            }

            return 0;
        }

        public static int sendCityToDB(City city)
        {
            Dictionary<string, string> parms = new Dictionary<string, string>();

            parms.Add(DBKeys.City.NAME, city.name);
            parms.Add(DBKeys.City.PROVINCE, city.province.ToString());

            string request = Functions.createRequest("location/add", parms);

            string response = Functions.readRequest(request);

            if (response != null)
            {
                return int.Parse(response);
            }

            return 0;
        }

        public static int sendAddressToDB(Address address)
        {
            Dictionary<string, string> parms = new Dictionary<string, string>();

            parms.Add(DBKeys.Address.STREET, address.street);
            parms.Add(DBKeys.Address.NUMBER, address.number);
            parms.Add(DBKeys.Address.ZIPCODE, address.zipCode);
            parms.Add(DBKeys.Address.COORDINATES, address.coordinates);
            parms.Add(DBKeys.Address.CITY, address.city.ToString());

            string request = Functions.createRequest("location/add", parms);

            string response = Functions.readRequest(request);

            if (response != null)
            {
                return int.Parse(response);
            }

            return 0;
        }

        /* END DATABASE FUNCTIONS */

        public static Country GetCountry(int _id)
        {
            if (countries == null)
            {
                countries = GetCountries();
            }

            return countries.Find(x => x.Equals(_id));
        }

        public static Province GetProvince(int _id)
        {
            if (provinces == null)
            {
                provinces = GetProvinces();
            }

            return provinces.Find(x => x.Equals(_id));
        }

        public static City GetCity(int _id)
        {
            if (cities == null)
            {
                cities = GetCities();
            }

            return cities.Find(x => x.Equals(_id));
        }

        public static Address GetAddress(int _id)
        {
            if (addresses == null)
            {
                addresses = GetAddresses();
            }

            return addresses.Find(x => x.Equals(_id));
        }
    }

}
