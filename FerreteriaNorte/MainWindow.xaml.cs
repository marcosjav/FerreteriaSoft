using FerreteriaNorte.Sales;
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
            Sale
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pages = new Dictionary<PageType, Page>();
            pages.Add(PageType.Main, new MainPage(this));
            pages.Add(PageType.Sales, new Page1(this));
            pages.Add(PageType.Sale, new Sale());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(pages[PageType.Sales]);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(pages[PageType.Main]);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(pages[PageType.Sale]);
        }

        /**  NAVIGATION **/
        public void Navigate(PageType pageType)
        {
            mainFrame.Navigate(pages[pageType]);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection mySql = new MySql.Data.MySqlClient.MySqlConnection();
            mySql.ConnectionString = "server=localhost; uid=root; pwd=12345678; database=norte;SslMode=none";

            try
            {
                mySql.Open();
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM `cliente` WHERE 1", mySql);
                MySql.Data.MySqlClient.MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                string clients = "";
                while (mySqlDataReader.Read())
                {
                    clients += mySqlDataReader["nombre"];
                    clients += '\n';
                }

                MessageBox.Show(clients);

                mySql.Close();
            }
            catch (Exception err)
            {
                mySql.Close();
                MessageBox.Show("Error de conexión: " + err.Message);
            }
            finally
            {
                mySql.Dispose();
            }

        }
    }
}
