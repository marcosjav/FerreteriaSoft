using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FerreteriaNorte.Classes.Items.Extra
{
    class StockHelper
    {
        /// <summary>
        /// Parse a Stock JSONObject to a Stock class object
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Returns NULL if JSONObject don't contains id and name of stock</returns>
        public static Stock parseStock(string str)
        {
            JObject jsonStock = JObject.Parse(str);
            string json_item_id = jsonStock[DBKeys.Stock.ITEM_ID].ToString();
            string json_shop_id = jsonStock[DBKeys.Stock.SHOP_ID].ToString();
            string json_unit_id = jsonStock[DBKeys.Stock.UNIT_ID].ToString();

            int item_id = 0, shop_id = 0, unit_id = 0;
            string quantity = jsonStock[DBKeys.Stock.QUANTITY_STOCK].ToString();
            string min = jsonStock[DBKeys.Stock.MIN_STOCK].ToString();

            if (int.TryParse(json_item_id, out item_id) &&
                int.TryParse(json_shop_id, out shop_id) &&
                int.TryParse(json_unit_id, out unit_id) && 
                quantity != null)
            {
                double q, m;
                if (!double.TryParse(quantity, out q))
                    q = 0.0;
                if (!double.TryParse(min, out m))
                    m = 0.0;

                return new Stock(q, m, unit_id, shop_id, item_id);
            }

            return null;
        }

        /// <summary>
        /// Call to DB API REST to get the Stock list
        /// </summary>
        /// <returns>A list of Stock objects</returns>
        public static List<Stock> GetStocks()
        {
            List<Stock> stocks = new List<Stock>();

            string request = Properties.Resources.base_url + "stock/list";

            string response = Functions.readRequest(request);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                Stock newStock = parseStock(str);
                if (newStock != null)
                {
                    stocks.Add(newStock);
                }
            }

            stocks.Sort();

            return stocks;
        }

        /// <summary>
        /// Load the list of stocks into a DataGridView
        /// </summary>
        /// <param name="datagrid">The DataGridView object</param>
        public static void setStockGrid(DataGrid datagrid, List<Stock> stocks = null)
        {
            if (stocks == null)
                stocks = GetStocks();

            datagrid.ItemsSource = stocks;

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
        /// Custom insert of Stock List in DataGrid
        /// </summary>
        /// <param name="datagrid">DataGrid Object</param>
        public static void LoadStocksGrid(DataGrid datagrid)
        {
            List<Stock> stocks = GetStocks();
            DataTable dt = new DataTable();

            CommonViewConfigurations.ConfigDataGrid(datagrid);

            // first add your columns
            dt.Columns.Add("Código");
            dt.Columns.Add("Marca");

            foreach (Stock item in stocks)
            {
                var row = dt.NewRow();
                // Set values for columns with row[i] = xy
                row[0] = item.item_id.ToString("D5");
                row[1] = item.quantity_stock;

                dt.Rows.Add(row);
            }

            datagrid.DataContext = dt;
        }

        public static int sendToDB(Stock stock)
        {
            string request = Properties.Resources.base_url + "stock/add";

            //request += "?name=" + stock.name;

            string response = Functions.readRequest(request);

            if (response != null)
            {
                return int.Parse(response);
            }

            return 0;
        }
    }
}
