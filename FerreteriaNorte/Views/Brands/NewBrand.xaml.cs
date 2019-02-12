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
    /// Lógica de interacción para NewBrand.xaml
    /// </summary>
    public partial class NewBrand : Page
    {
        public NewBrand()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text.Length > 2)
            {
                Brand brand = new Brand(textBoxName.Text);
                int r = brand.sendToDB();
                MessageBox.Show(r.ToString());
                if (r > 0)
                {
                    textBoxName.Text = "";
                }
            }
        }
    }
}
