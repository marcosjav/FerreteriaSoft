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
    /// Lógica de interacción para MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private MainWindow mainWindow;

        public MainPage(MainWindow window)
        {
            InitializeComponent();
            this.mainWindow = window;
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mainWindow.Navigate(MainWindow.PageType.Sales);
        }
    }
}
