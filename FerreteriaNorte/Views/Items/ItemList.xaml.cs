using FerreteriaNorte.Classes.Items;
using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FerreteriaNorte.Views.Items
{
    /// <summary>
    /// Lógica de interacción para List.xaml
    /// </summary>
    public partial class ItemList : Page
    {
        public ItemList()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //ItemHelper.setItemsGrid(dgItems);

            ItemHelper.LoadItemsGrid(dgItems);

            //List<Item> items = new List<Item>();

            //string request = Properties.Resources.base_url + "item/list";

            //string response = Functions.readRequest(request);

            //JArray jsonArray = JArray.Parse(response);

            //foreach (JObject item in jsonArray)
            //{
            //    //string str = item.ToString();
            //    //Item newItem = parseItem(str);
            //    Item newItem = ItemHelper.parseItem(item);
            //    if (newItem != null)
            //    {
            //        items.Add(newItem);
            //        MessageBox.Show(newItem.ToString());
            //    }
            //}
            //dgItems.ItemsSource = items;

        }

        private void dgItems_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
