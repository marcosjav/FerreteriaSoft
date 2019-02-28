using FerreteriaNorte.Classes.Brands;
using FerreteriaNorte.Classes.Companies;
using FerreteriaNorte.Classes.Extra;
using FerreteriaNorte.Classes.Shops;
using FerreteriaNorte.Views.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FerreteriaNorte.Views.Items
{
    /// <summary>
    /// Lógica de interacción para NewItem.xaml
    /// </summary>
    public partial class NewItem : Page
    {
        private List<Company.CompanyItem> companies = new List<Company.CompanyItem>();
        private List<Subtitle> subtitles = new List<Subtitle>();
        private List<Brand> brands = new List<Brand>();

        public NewItem()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            comboBrand.ItemsSource = BrandHelper.GetBrands();

            comboTitle.ItemsSource = TitleHelper.GetTitles();

            comboCompany.ItemsSource = CompanyHelper.GetCompanies();

            comboShop.ItemsSource = ShopHelper.GetShops();

            dataGridCompanies.HeadersVisibility = DataGridHeadersVisibility.None;

            loadCompanies();
        }

        private void comboTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboTitle.SelectedIndex >= 0)
            {
                comboSubtitle.ItemsSource = TitleHelper.GetSubtitles((int)((Title)comboTitle.SelectedItem).Value);
            }
        }

        private void buttonImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog() { Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg" };
            if (open.ShowDialog() == DialogResult.OK)
            {
                imageBox.Source = new BitmapImage(new Uri(open.FileName));
            }
        }

        private void comboCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboCompany.SelectedIndex > -1)
            {
                textCode.IsEnabled = true;
            }

        }

        private void loadCompanies()
        {
            List<Company> allCompanies = CompanyHelper.GetCompanies();
            List<Company> companiesCombo = new List<Company>();

            foreach (Company item in allCompanies)
            {
                if (!companies.Contains(item))
                {
                    companiesCombo.Add(item);
                }
            }
            comboCompany.ItemsSource = companiesCombo;
            comboCompany.SelectedIndex = -1;
        }

        private void dataGridCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridCompanies.SelectedIndex > -1)
            {
                System.Windows.Forms.MessageBox.Show(((Company)dataGridCompanies.SelectedItem).Value.ToString());
            }
        }

        private void buttonAddCompany_Click(object sender, RoutedEventArgs e)
        {
            if (comboCompany.SelectedIndex > -1 && textCode.Text.Length > 2)
            {
                Company.CompanyItem item = new Company.CompanyItem((Company)comboCompany.SelectedItem, 0, textCode.Text);
                companies.Add(item);
                textCode.Text = "";
                loadCompanies();
            }

            dataGridCompanies.ItemsSource = null;
            dataGridCompanies.ItemsSource = companies;

            dataGridCompanies.Columns[1].Visibility = Visibility.Hidden;
            dataGridCompanies.Columns[0].Width = dataGridCompanies.Width - 5;
            dataGridCompanies.IsReadOnly = true;
        }

        private void buttonAddBrand_Click(object sender, RoutedEventArgs e)
        {
            Brand brand = (Brand)comboBrand.SelectedItem;

            if (!brands.Contains(brand))
                brands.Add(brand);

            BrandHelper.setBrandGrid(dataGridBrands, brands);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonAddSubTitle_Click(object sender, RoutedEventArgs e)
        {
            Subtitle subtitle = (Subtitle)comboSubtitle.SelectedItem;

            if (!subtitles.Contains(subtitle))
                subtitles.Add(subtitle);

            TitleHelper.setSubtitleGrid(dataGridCategories, subtitles);
        }
    }
}
