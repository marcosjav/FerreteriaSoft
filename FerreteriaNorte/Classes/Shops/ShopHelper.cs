using FerreteriaNorte.Classes.Locations;
using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FerreteriaNorte.Classes.Shops
{
    class ShopHelper
    {
        public static Shop parseShop(string str)
        {
            JObject jsonShop = JObject.Parse(str);

            int id = 0, id_address = 0;
            string json_id = jsonShop[DBKeys.Shop.ID].ToString();
            string json_name = jsonShop[DBKeys.Shop.NAME].ToString();
            string json_address_id = jsonShop[DBKeys.Shop.ADDRESS].ToString();

            if (int.TryParse(json_id, out id) && json_name != null && int.TryParse(json_address_id, out id_address))
            {
                string json_address = jsonShop[DBKeys.Address.STREET].ToString() ?? "";
                json_address += " ";
                json_address += jsonShop[DBKeys.Address.NUMBER].ToString() ?? "";
                json_address += " - ";
                json_address += jsonShop[DBKeys.City.NAME].ToString() ?? "";
                json_address += " - ";
                json_address += jsonShop[DBKeys.Province.NAME].ToString() ?? "";
                json_address += " - ";
                json_address += jsonShop[DBKeys.Country.NAME].ToString() ?? "";

                if (json_address != null && json_address.Length > 1)
                {
                    return new Shop(id, json_name, id_address, json_address);
                }
                return new Shop(id, json_name, id_address);
            }

            return null;
        }

        public static List<Shop> GetShops()
        {
            List<Shop> shops = new List<Shop>();

            string request = Properties.Resources.base_url + "shop/list";

            string response = Functions.readRequest(request);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                Shop newShop = parseShop(str);
                if (newShop != null)
                {
                    shops.Add(newShop);
                }
            }

            shops.Sort();

            return shops;
        }

        public static List<Shop> GetFullShops()
        {
            List<Shop> list = new List<Shop>();

            string request = Functions.createRequest("shop/fulllist", new Dictionary<string, string>());

            string response = Functions.readRequest(request);

            JArray jArray = JArray.Parse(response);

            foreach (JObject item in jArray)
            {
                string str = item.ToString();
                Shop newShop = parseShop(str);
                if (newShop != null)
                {
                    list.Add(newShop);
                }
            }

            list.Sort();

            return list;
        }

        public static void setShopGrid(DataGrid datagrid)
        {
            List<Shop> shops = GetFullShops();
            datagrid.ItemsSource = shops;

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
                    case "street":
                        item.Header = "Dirección";
                        item.DisplayIndex = 1;
                        break;
                    default:
                        item.Visibility = System.Windows.Visibility.Hidden;
                        break;
                }
            }
        }

        public static void setShopGrid2(DataGrid datagrid)
        {
            List<Shop> shops = GetFullShops();
            datagrid.ItemsSource = shops;

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
                    case "address":
                        item.Header = "Dirección";
                        item.DisplayIndex = 1;
                        break;
                    default:
                        item.Visibility = System.Windows.Visibility.Hidden;
                        break;
                }
            }
        }

    }
}
