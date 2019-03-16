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

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "btnAddCountry":
                    addCountry();
                    break;
                case "btnAddProvince":
                    addProvince();
                    break;
                case "btnAddCity":
                    addCity();
                    break;
                default:
                    break;
            }
        }

        private void dataGridCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadProvinces();
        }

        private void dataGridProvince_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadCities();
        }

        private void LoadCountries()
        {
            AddressHelper.setCountryGrid(dataGridCountry);
        }

        private void LoadProvinces()
        {
            if (dataGridCountry.SelectedItem != null)
            {
                Country c = (Country)dataGridCountry.SelectedItem;

                AddressHelper.setCityGrid(dataGridCity, 0, c.id);
                AddressHelper.setProvinceGrid(dataGridProvince, c.id);
            }
        }

        private void LoadCities()
        {
            if (dataGridProvince.SelectedItem != null)
            {
                Province p = (Province)dataGridProvince.SelectedItem;

                AddressHelper.setCityGrid(dataGridCity, p.id);
            }
        }
        private void addCountry()
        {
            if (tbCountry.Text.Length > 2)
            {
                Country country = new Country(tbCountry.Text);
                int countryId = AddressHelper.sendCountryToDB(country);
                if (countryId > 0)
                {
                    MessageBox.Show("Se guardó con el código: " + countryId.ToString());
                }
                else
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

        private void addProvince()
        {
            if (tbProvince.Text.Length > 2 && dataGridCountry.SelectedItem != null)
            {
                Country c = (Country)dataGridCountry.SelectedItem;
                Province province = new Province(tbProvince.Text, c.id);

                int provinceId = AddressHelper.sendProvinceToDB(province);
                if (provinceId > 0)
                {
                    MessageBox.Show("Se guardó con el código: " + provinceId.ToString());
                }
                else
                {
                    MessageBox.Show("Ocurrió un error y no pudo guardarse, intente más tarde");
                }
                LoadProvinces();
            }
            else if (dataGridCountry.SelectedItem != null)
            {
                MessageBox.Show("Seleccione País");
            }
            else
            {
                MessageBox.Show("Nombre muy corto");
            }
        }

        private void addCity()
        {
            if (tbCity.Text.Length > 2 && dataGridProvince.SelectedItem != null)
            {
                Province p = (Province)dataGridProvince.SelectedItem;
                City city = new City(tbCity.Text, p.id);

                int cityId = AddressHelper.sendCityToDB(city);
                if (cityId > 0)
                {
                    MessageBox.Show("Se guardó con el código: " + cityId.ToString());
                }
                else
                {
                    MessageBox.Show("Ocurrió un error y no pudo guardarse, intente más tarde");
                }
                LoadCities();
            }
            else if (dataGridProvince.SelectedItem != null)
            {
                MessageBox.Show("Seleccione Provincia");
            }
            else
            {
                MessageBox.Show("Nombre muy corto");
            }
        }
    }
}
