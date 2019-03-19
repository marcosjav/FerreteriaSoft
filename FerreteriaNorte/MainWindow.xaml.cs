using FerreteriaNorte.Classes;
using FerreteriaNorte.Classes.Clients;
using FerreteriaNorte.Resources.Utils;
using FerreteriaNorte.Sales;
using FerreteriaNorte.Views.Brands;
using FerreteriaNorte.Views.Companies;
using FerreteriaNorte.Views.General;
using FerreteriaNorte.Views.Items;
using FerreteriaNorte.Views.Locations;
using FerreteriaNorte.Views.Shops;
using FerreteriaNorte.Views.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FerreteriaNorte
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<PageType, Page> pages;
        public enum PageType
        {
            Main,
            Sales,
            NewSale,
            NewItem,
            ItemList,
            NewBrand,
            BrandList,
            NewCompany,
            CompanyList,
            Settings,
            Shops,
            NewShop,
            Locations,
            Titles
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pages = new Dictionary<PageType, Page>();
            pages.Add(PageType.Main, new MainPage(this));
            
            pages.Add(PageType.NewSale, new Sale());
            pages.Add(PageType.NewItem, new NewItem());
            pages.Add(PageType.ItemList, new ItemList());
            pages.Add(PageType.NewBrand, new NewBrand());
            pages.Add(PageType.BrandList, new BrandList());
            pages.Add(PageType.NewCompany, new NewCompany());
            pages.Add(PageType.CompanyList, new CompanyList());
            pages.Add(PageType.Settings, new UserSettings());
            pages.Add(PageType.Shops, new ShopList());
            pages.Add(PageType.NewShop, new NewShop());
            pages.Add(PageType.Locations, new Locations());
            pages.Add(PageType.Titles, new Titles());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "btnNewBrand":
                    mainFrame.Navigate(pages[PageType.NewBrand]);
                    break;
                case "btnNewCompany":
                    mainFrame.Navigate(pages[PageType.NewCompany]);
                    break;
                case "btnNewItem":
                    mainFrame.Navigate(pages[PageType.NewItem]);
                    break;
                case "btnSettings":
                    mainFrame.Navigate(pages[PageType.Settings]);
                    break;
                case "btnItemList":
                    mainFrame.Navigate(pages[PageType.ItemList]);
                    //string request = Properties.Resources.base_url + "item/list";

                    //string response = Functions.readRequest(request);
                    //MessageBox.Show(response);

                    break;
                case "btnNewSale":
                    mainFrame.Navigate(pages[PageType.NewSale]);
                    break;
                case "btnBrandList":
                    mainFrame.Navigate(pages[PageType.BrandList]);
                    break;
                case "btnShopList":
                    mainFrame.Navigate(pages[PageType.Shops]);
                    break;
                case "btnNewShop":
                    mainFrame.Navigate(pages[PageType.NewShop]);
                    break;
                case "btnCompanyList":
                    mainFrame.Navigate(pages[PageType.CompanyList]);
                    break;
                case "btnNewLocations":
                    mainFrame.Navigate(pages[PageType.Locations]);
                    break;
                case "btnTitles":
                    mainFrame.Navigate(pages[PageType.Titles]);
                    break;
                default:
                    break;
            }
            
            //if (Properties.Settings.Default.first_time)
            //{
            //MessageBox.Show(Properties.Resources.base_url);
            //}
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(pages[PageType.Main]);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(pages[PageType.NewSale]);
        }

        /**  NAVIGATION **/
        public void Navigate(PageType pageType)
        {
            mainFrame.Navigate(pages[pageType]);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client();
            //client.Id = Convert.ToInt32(dataReader["Id"]);
            client.Documento = 32729321;
            client.Nombre = "Prueba .NET";
            client.Direccion = "Microsoft";
            client.Provincia = "Visual Studio";
            client.Pais = "EEUU";
            client.CodigoPostal = "0303456";
            client.Condiciones = "Contado";
            client.Celular = "0101010101233";
            client.Telefono = "0303456";
            client.Email = "email@domain.com";
            client.Limite = "999.99";
            client.Lista = "lista1";
            client.FechaInicio = DateTime.Now;

           
            MessageBox.Show("succeful");
            //try
            //{
                
            //}
            //catch (Exception er)
            //{
            //    //MessageBox.Show("Error: " + er.Message);
            //    throw;
            //}
            
        }
    }
}
