using System;
using System.Collections.Generic;
using System.Data;
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
using FerreteriaNorte.Classes.Companies;
using FerreteriaNorte.Views.General;

namespace FerreteriaNorte.Views.Companies
{
    /// <summary>
    /// Lógica de interacción para CompanyList.xaml
    /// </summary>
    public partial class CompanyList : Page
    {
        List<Company> companies;

        public CompanyList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //CompanyHelper.setCompanyGrid(dataGridCompany);
            companies = CompanyHelper.LoadCompanyGrid(dataGridCompany);
        }

        private void dataGridCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show(((Company)dataGridCompany.SelectedItem).getFullDescription());
            DataRowView row = dataGridCompany.SelectedItem as DataRowView;
            int itemId = int.Parse(row.Row.ItemArray[0].ToString());
            Company company = CompanyHelper.GetCompany(itemId);
            MessageBox.Show(company.getData(), company.getTitle());
            //CustomMessageBox
        }
    }
}
