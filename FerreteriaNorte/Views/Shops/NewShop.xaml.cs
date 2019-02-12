using FerreteriaNorte.Classes.Locations;
using FerreteriaNorte.Classes.Shops;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FerreteriaNorte.Views.Shops
{
    /// <summary>
    /// Lógica de interacción para NewShop.xaml
    /// </summary>
    public partial class NewShop : Page
    {
        public NewShop()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            JObject jObject = new JObject();

            Shop shop = new Shop();
            shop.name = textBoxName.Text;

            Address address = new Address();
            address.street = textBoxStreet.Text;
            address.number = textBoxNumber.Text;
            address.city = comboBoxCity.SelectedValue;
        }
    }
}
