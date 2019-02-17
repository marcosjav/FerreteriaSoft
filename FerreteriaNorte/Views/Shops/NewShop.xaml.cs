using FerreteriaNorte.Classes.Extra;
using FerreteriaNorte.Classes.Locations;
using FerreteriaNorte.Classes.Shops;
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

namespace FerreteriaNorte.Views.Shops
{
    /// <summary>
    /// Lógica de interacción para NewShop.xaml
    /// </summary>
    public partial class NewShop : Page
    {
        List<Phone> phones = new List<Phone>();
        List<StringValue> emails = new List<StringValue>();

        public NewShop()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxCountry.ItemsSource = AddressHelper.GetCountry();

            comboBoxPhoneType.ItemsSource = PhoneHelper.GetPhoneTypes();
        }

        private void comboBoxCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxCountry.SelectedIndex >= 0)
            {
                comboBoxProvince.ItemsSource = AddressHelper.GetProvince(((Country)comboBoxCountry.SelectedItem).id);
            }
        }

        private void comboBoxProvince_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxProvince.SelectedIndex >= 0)
            {
                comboBoxCity.ItemsSource = AddressHelper.GetCity(((Province)comboBoxProvince.SelectedItem).id);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            JObject jShop = new JObject();

            Shop shop = new Shop();
            shop.name = textBoxName.Text;

            jShop.Add(DBKeys.Shop.NAME, shop.name);

            Address address = new Address();
            address.street = textBoxStreet.Text;
            address.number = textBoxNumber.Text;
            address.city = ((City)comboBoxCity.SelectedItem).id;

            JObject jAddres = new JObject();
            jAddres.Add(DBKeys.Address.STREET, address.street);
            jAddres.Add(DBKeys.Address.NUMBER, address.number);
            jAddres.Add(DBKeys.Address.CITY, address.city);

            jShop.Add("address", jAddres);

            JArray jPhones = new JArray();

            foreach (Phone phone in phones)
            {
                JObject jPhone = new JObject();

                jPhone.Add(DBKeys.Phone.AREA, phone.area);
                jPhone.Add(DBKeys.Phone.NUMBER, phone.number);
                jPhone.Add(DBKeys.Phone.TYPE, phone.type);

                jPhones.Add(jPhone);
            }

            jShop.Add("phones", jPhones);

            JArray jEmails = new JArray();

            foreach (StringValue email in emails)
            {
                jEmails.Add(email.Value);
            }

            jShop.Add("emails", jEmails);

            Tuple<HttpStatusCode, string> response = Functions.sendPost(Properties.Resources.base_url + "shop/add", jShop.ToString());

            JObject jres = JObject.Parse(response.Item2);
            if (response.Item1 == HttpStatusCode.OK && jres.ContainsKey(DBKeys.Shop.ID))
            {
                MessageBox.Show("Se guardó correctamente");
                btnClear_Click(btnClear, null);
            }
                
            else
                MessageBox.Show("No se pudo guardar");
        }

        private void btnAddPhone_Click(object sender, RoutedEventArgs e)
        {
            Phone phone = new Phone(textBoxPhoneNumber.Text, textBoxPhoneArea.Text, ((PhoneType)comboBoxPhoneType.SelectedItem).id, comboBoxPhoneType.Text);

            textBoxPhoneNumber.Text = "";
            textBoxPhoneArea.Text = "";

            phones.Add(phone);

            PhoneHelper.setPhoneGrid(dataGridPhones, phones);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxName.Text = "";
            textBoxStreet.Text = "";
            textBoxNumber.Text = "";
            textBoxPhoneArea.Text = "";
            textBoxPhoneNumber.Text = "";

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
    }

    public class StringValue
    {
        public StringValue(string s)
        {
            _value = s;
        }
        public string Value { get { return _value; } set { _value = value; } }
        string _value;
    }
}
