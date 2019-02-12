using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FerreteriaNorte.Classes.Brands
{
    class BrandHelper
    {

        /// <summary>
        /// Parse a Brand JSONObject to a Brand class object
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Returns NULL if JSONObject don't contains id and name of brand</returns>
        public static Brand parseBrand(string str)
        {
            JObject jsonBrand = JObject.Parse(str);
            string json_id = jsonBrand[DBKeys.Brand.ID].ToString();

            int id = 0;
            string name = jsonBrand[DBKeys.Brand.NAME].ToString();

            if (int.TryParse(json_id, out id) && name != null)
            {
                return new Brand(id, name);
            }

            return null;
        }

        /// <summary>
        /// Call to DB API REST to get the Brand list
        /// </summary>
        /// <returns>A list of Brand objects</returns>
        public static List<Brand> GetBrands()
        {
            List<Brand> brands = new List<Brand>();

            string request = Properties.Resources.base_url + "brand/list";

            string response = Functions.readRequest(request);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                Brand newBrand = parseBrand(str);
                if (newBrand != null)
                {
                    brands.Add(newBrand);
                }
            }

            brands.Sort();

            return brands;
        }

        /// <summary>
        /// Load the list of brands into a DataGridView
        /// </summary>
        /// <param name="datagrid">The DataGridView object</param>
        public static void setBrandGrid(DataGrid datagrid)
        {
            List<Brand> brands = GetBrands();
            datagrid.ItemsSource = brands;

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
                    default:
                        item.Visibility = System.Windows.Visibility.Hidden;
                        break;
                }
            }

        }

        /// <summary>
        /// Custom insert of Brand List in DataGrid
        /// </summary>
        /// <param name="datagrid">DataGrid Object</param>
        public static void LoadBrandsGrid(DataGrid datagrid)
        {
            List<Brand> brands = GetBrands();
            DataTable dt = new DataTable();

            CommonViewConfigurations.ConfigDataGrid(datagrid);

            // first add your columns
            dt.Columns.Add("Código");
            dt.Columns.Add("Marca");

            foreach (Brand item in brands)
            {
                var row = dt.NewRow();
                // Set values for columns with row[i] = xy
                row[0] = item.id.ToString("D5");
                row[1] = item.name;

                dt.Rows.Add(row);
            }

            datagrid.DataContext = dt;
        }

        public static int sendToDB(Brand brand)
        {
            string request = Properties.Resources.base_url + "brand/add";

            request += "?name=" + brand.name;

            string response = Functions.readRequest(request);

            if (response != null)
            {
                return int.Parse(response);
            }

            return 0;
        }
    }
}
