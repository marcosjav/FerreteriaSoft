using FerreteriaNorte.Classes.Brands;
using FerreteriaNorte.Classes.Companies;
using FerreteriaNorte.Classes.Extra;
using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FerreteriaNorte.Classes.Items
{
    class ItemHelper
    {
        /*
         
        
        
        company_id
        item_id
        code
        cost_item
        id_company
        name_company
        last_update_company
        web_company
        cuit_company
        item_id
        subtitle_id
        id_subtitle
        name_subtitle
        title_id
        id_title
        name_title
        shop_id
        shop_address_id
        item_id
        quantity_stock
        min_stock
        unit_id
        id_unit
        name_unit
        id_shop
        name_shop
        address_id
        brand_id
        item_id
        id_brand
        name_brand
         */

        /// <summary>
        /// Parse a Item JSONObject to a Item class object
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Returns NULL if JSONObject don't contains Item</returns>
        public static Item parseItem(object o)
        {
            JObject jsonItem = null;
            string json_id = "";

            if (o.GetType() == typeof(string))
            {
                jsonItem = JObject.Parse((string)o);
            }
            else if ((o.GetType() == typeof(JObject)))
            {
                jsonItem = (JObject)o;
            }

            if (jsonItem != null)
            {
                int id = 0;
                string name = jsonItem[DBKeys.Item.NAME].ToString();

                json_id = jsonItem[DBKeys.Item.ID].ToString();

                if (int.TryParse(json_id, out id) && name != null)
                {
                    return new Item(id, name, jsonItem[DBKeys.Item.DESCRIPTION].ToString());
                }
            }

            return null;
        }

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
        /// Call to DB API REST to get the Items list
        /// </summary>
        /// <returns>A list of Item objects</returns>
        public static List<Item> GetItems()
        {
            List<Item> items = new List<Item>();

            string request = Properties.Resources.base_url + "item/list";

            string response = Functions.readRequest(request);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                //string str = item.ToString();
                //Item newItem = parseItem(str);
                Item newItem = parseItem(item);
                if (newItem != null)
                {
                    items.Add(newItem);
                }
            }

            items.Sort();

            return items;
        }

        /// <summary>
        /// Load the list of brands into a DataGridView
        /// </summary>
        /// <param name="datagrid">The DataGridView object</param>
        public static void setItemsGrid(System.Windows.Controls.DataGrid datagrid)
        {
            List<Item> items = GetItems();
            datagrid.ItemsSource = items;

            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            datagrid.IsReadOnly = true;

            /*
                int id;
                string name, description;
                List<Brand> brands { get; set; }
                List<Company> companies { get; set; }
                List<Picture> pictures { get; set; }
                List<Subtitle> subtitles { get; set; }
            */
            foreach (DataGridColumn column in datagrid.Columns)
            {
                switch ((string)column.Header)
                {
                    case "Value":
                        column.Header = "Código";
                        column.DisplayIndex = 0;
                        break;
                    case "Text":
                        column.Header = "Artículo";
                        column.DisplayIndex = 1;
                        break;
                    default:
                        column.Visibility = System.Windows.Visibility.Hidden;
                        break;
                }
            }

        }

        /// <summary>
        /// Custom insert of Item List in DataGrid
        /// </summary>
        /// <param name="datagrid">DataGrid Object</param>
        public static void LoadItemsGrid(DataGrid datagrid)
        {
            List<Item> items = GetItems();
            DataTable dt = new DataTable();

            CommonViewConfigurations.ConfigDataGrid(datagrid);

            // first add your columns
            dt.Columns.Add("Código");
            dt.Columns.Add("Artículo");
            dt.Columns.Add("Descripción");

            foreach (Item item in items)
            {
                var row = dt.NewRow();
                // Set values for columns with row[i] = xy
                row[0] = item.getId().ToString("D5");
                row[1] = item.getName();
                row[2] = item.getDescription();

                dt.Rows.Add(row);
            }

            datagrid.DataContext = dt;
        }

    }
}
