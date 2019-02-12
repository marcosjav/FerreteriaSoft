﻿using FerreteriaNorte.Classes.Shops;
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
    /// Lógica de interacción para ShopList.xaml
    /// </summary>
    public partial class ShopList : Page
    {
        public ShopList()
        {
            InitializeComponent();
        }

        private void dataGridShops_Loaded(object sender, RoutedEventArgs e)
        {
            //dataGridShops.ItemsSource = ShopHelper.GetFullShops();
            ShopHelper.setShopGrid2(dataGridShops);
        }
    }
}
