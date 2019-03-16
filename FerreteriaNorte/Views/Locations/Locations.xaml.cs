using FerreteriaNorte.Classes.Locations;
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

namespace FerreteriaNorte.Views.Locations
{
    /// <summary>
    /// Lógica de interacción para Locations.xaml
    /// </summary>
    public partial class Locations : Page
    {
        public Locations()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCountries();
        }

        private void btnAddCountry_Click(object sender, RoutedEventArgs e)
        {
            if (tbCountry.Text.Length > 2)
            {
                Country country = new Country(tbCountry.Text);
                int countryId = AddressHelper.sendCountryToDB(country);
                if (countryId > 0)
                {
                    MessageBox.Show("Se guardó con el código: " + countryId.ToString());
                } else
                {
                    MessageBox.Show("Ocurrió un error y no pudo guardarse, intente más tarde");
                }
                LoadCountries();
            }   
            else
            {
                MessageBox.Show("Nombre muy corto");
            }
        }

        private void dataGridCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridCountry.SelectedItem != null)
            {
                Country c = (Country)dataGridCountry.SelectedItem;

                AddressHelper.setCityGrid(dataGridCity, 0, c.id);
                AddressHelper.setProvinceGrid(dataGridProvince, c.id);
            }
        }

        private void dataGridProvince_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridProvince.SelectedItem != null)
            {
                Province p = (Province)dataGridProvince.SelectedItem;

                AddressHelper.setCityGrid(dataGridCity, p.id);
            }
        }

        private void LoadCountries()
        {
            AddressHelper.setCountryGrid(dataGridCountry);
        }
    }
}
