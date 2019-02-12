using FerreteriaNorte.Classes.Brands;
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

namespace FerreteriaNorte.Views.Brands
{
    /// <summary>
    /// Lógica de interacción para BrandList.xaml
    /// </summary>
    public partial class BrandList : Page
    {
        public BrandList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //BrandHelper.setBrandGrid(dgBrand);
            BrandHelper.LoadBrandsGrid(dgBrand);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void dgBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
