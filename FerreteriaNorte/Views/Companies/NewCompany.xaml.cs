using FerreteriaNorte.Classes.Companies;
using FerreteriaNorte.Classes.Extra;
using FerreteriaNorte.Classes.Locations;
using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace FerreteriaNorte.Views.Companies
{
    /// <summary>
    /// Lógica de interacción para NewCompany.xaml
    /// </summary>
    public partial class NewCompany : Page
    {
        static List<Phone> phones = new List<Phone>();
        static List<StringValue> emails = new List<StringValue>();
        static List<Address> addresses = new List<Address>();

        public NewCompany()
        {
            InitializeComponent();
        }
        /*  START AND COMBOBOX LOADING SECTION   */
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxCountry.ItemsSource = AddressHelper.GetCountries();

            comboBoxPhoneType.ItemsSource = PhoneHelper.GetPhoneTypes();
        }

        private void comboBoxCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxCountry.SelectedIndex >= 0)
            {
                comboBoxProvince.ItemsSource = AddressHelper.GetProvinces(((Country)comboBoxCountry.SelectedItem).id);
            }
        }

        private void comboBoxProvince_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxProvince.SelectedIndex >= 0)
            {
                comboBoxCity.ItemsSource = AddressHelper.GetCities(((Province)comboBoxProvince.SelectedItem).id);
            }
        }

        /*  END START AND COMBOBOX LOADING SECTION   */

        /*  COMBOBOX ACTION SECTION   */

        private void btnAddPhone_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxPhoneNumber.Text.Length > 0 && textBoxPhoneArea.Text.Length > 0 && comboBoxPhoneType.SelectedItem != null)
            {
                Phone phone = new Phone(textBoxPhoneNumber.Text, textBoxPhoneArea.Text, ((PhoneType)comboBoxPhoneType.SelectedItem).id, comboBoxPhoneType.Text);

                textBoxPhoneNumber.Text = "";
                textBoxPhoneArea.Text = "";

                phones.Add(phone);

                PhoneHelper.setPhoneGrid(dataGridPhones, phones);

            }
        }

        private void btnAddEmail_Click(object sender, RoutedEventArgs e)
        {
            if (Functions.IsValidEmail(textBoxEmail.Text))
            {
                emails.Add(new StringValue(textBoxEmail.Text));
                textBoxEmail.Text = "";
                dataGridEmail.ItemsSource = null;
                dataGridEmail.ItemsSource = emails;
            }
        }

        private void btnAddAddress_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxStreet.Text.Length > 0 && textBoxNumber.Text.Length > 0 && comboBoxCity.SelectedValue != null)
            {
                Address address = new Address(textBoxStreet.Text, textBoxNumber.Text, "", "", ((City)comboBoxCity.SelectedValue).id);
                addresses.Add(address);

                AddressHelper.setAddressGrid(dataGridAddresses, addresses);

                textBoxStreet.Text = "";
                textBoxNumber.Text = "";
            }
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Company company = new Company(textBoxName.Text, textBoxWeb.Text, textBoxCUIT.Text, addresses, phones, toStringList(emails));

            Tuple<HttpStatusCode, string> response = CompanyHelper.sendToDB(company);

            JObject jres = JObject.Parse(response.Item2);
            if (response.Item1 == HttpStatusCode.OK && jres.ContainsKey(DBKeys.Company.ID))
            {
                MessageBox.Show("Se guardó correctamente");
                btnClear_Click(btnClear, null);
            }

            else
                MessageBox.Show("No se pudo guardar");
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "Texto|*.txt";
            //saveFileDialog.ShowDialog();
            //if (saveFileDialog.FileName != "")
            //{
            //    System.IO.File.AppendAllText(saveFileDialog.FileName, company.toJSON().ToString());
            //}
        }

        private List<string> toStringList(List<StringValue> list)
        {
            List<string> outList = new List<string>();

            foreach (StringValue item in list)
            {
                outList.Add(item.Value);
            }
            return outList;
        }

        /*  END COMBOBOX ACTION SECTION   */
    }
}
