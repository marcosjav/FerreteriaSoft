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

namespace FerreteriaNorte.Classes.Companies
{
    class Company : IComparable, IEquatable<int>
    {
        public int id { get; set; }
        public string name { get; set; }
        public string web { get; set; }
        public string cuit { get; set; }
        public List<Address> addresses { get; set; }
        public List<Phone> phones { get; set; }
        public List<string> emails { get; set; }

        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        public Company()
        {
        }

        public Company(string name, string web, string cuit)
        {
            this.Value = id;
            this.name = name;
            this.Text = name;
            this.web = web;
            this.cuit = cuit;
            setValues();
        }

        public Company(int id, string name, string web, string cuit) : this(name, web, cuit)
        {
            this.id = id;
        }

        public Company(string name, string web, string cuit, List<Address> addresses, List<Phone> phones, List<string> emails) : this(name, web, cuit)
        {
            this.addresses = addresses;
            this.phones = phones;
            this.emails = emails;
        }

        public Company(int id, string name, string web, string cuit, List<Address> addresses, List<Phone> phones, List<string> emails) : this(id, name, web, cuit)
        {
            this.addresses = addresses;
            this.phones = phones;
            this.emails = emails;
        }

        public override int GetHashCode()
        {
            var hashCode = -360538224;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(cuit);
            return hashCode;
        }

        // This functions are used to combobox
        public override string ToString()
        {
            return Text;
        }

        public string getFullDescription()
        {
            return getTitle() + Environment.NewLine + Environment.NewLine + getData();
            
        }

        public string getTitle()
        {
            return id + " - " + name;
        }

        public string getData()
        {
            string data = "";
            data += "CIUT:" + Environment.NewLine + "    " + cuit + Environment.NewLine + Environment.NewLine;
            data += "Dirección:" + Environment.NewLine;
            foreach (Address address in addresses)
            {
                data += "    " + address.ToString()  + Environment.NewLine;
            }
            
            data += Environment.NewLine + "Web:" + Environment.NewLine + "    " + web + Environment.NewLine + Environment.NewLine;
            data += "Teléfonos:" + Environment.NewLine;
            foreach (Phone phone in phones)
            {
                //data += "     (" + PhoneHelper.GetPhoneType(phone.type).name + ") " + phone.area + phone.number + Environment.NewLine;
                data += "     " + phone.ToString() + Environment.NewLine;
            }
            data += Environment.NewLine + "Emails:" + Environment.NewLine;
            foreach (string email in emails)
            {
                data += "    " + email + Environment.NewLine;
            }
            return data;
        }

        public int CompareTo(object obj)
        {
            return name.CompareTo(((Company)obj).name);
        }

        private void setValues()
        {
            this.Value = this.id;
            this.Text = this.name;
        }

        public JObject toJSON()
        {
            JObject jCompany = new JObject();

            JArray jEmails = new JArray();
            JArray jPhones = new JArray();
            JArray jAddresses = new JArray();

            jCompany.Add(DBKeys.Company.NAME, name);
            jCompany.Add(DBKeys.Company.CUIT, cuit);
            jCompany.Add(DBKeys.Company.WEB, web);

            foreach (string item in emails)
            {
                jEmails.Add(item);
            }

            foreach (Phone item in phones)
            {
                JObject jPhone = new JObject();

                jPhone.Add(DBKeys.Phone.AREA, item.area);
                jPhone.Add(DBKeys.Phone.NUMBER, item.number);
                jPhone.Add(DBKeys.Phone.TYPE, item.type);

                jPhones.Add(jPhone);
            }

            foreach (Address item in addresses)
            {
                JObject jAddress = new JObject();

                jAddress.Add(DBKeys.Address.STREET, item.street);
                jAddress.Add(DBKeys.Address.NUMBER, item.number);
                jAddress.Add(DBKeys.Address.ZIPCODE, item.zipCode);
                jAddress.Add(DBKeys.Address.COORDINATES, item.coordinates);
                jAddress.Add(DBKeys.Address.CITY, item.city);

                jAddresses.Add(jAddress);
            }

            jCompany.Add(DBKeys.Company.EMAIL_LIST, jEmails);
            jCompany.Add(DBKeys.Company.PHONE_LIST, jPhones);
            jCompany.Add(DBKeys.Company.ADDRESS_LIST, jAddresses);

            return jCompany;
        }

        public bool Equals(int _id)
        {
            return this.id == _id;
        }

        //public Tuple<HttpStatusCode, string> sendToDB(JObject jCompany)
        //{
        //    return Functions.sendPost(Properties.Resources.base_url + "company/add", jCompany.ToString());
        //}

        /*   Usado para los combobox  */
        public class CompanyItem : Company
        {
            string code { get; set; }
            int id_item { get; set; }

            public CompanyItem(Company company, int id_item, string code) : base(company.id, company.name, company.web, company.cuit)
            {
                this.code = code;
                this.id_item = id_item;

                this.Text = code + " - " + company.name;
            }

            public Company getCompany()
            {
                return (Company)this;
            }

            public class Discount : CompanyItem
            {
                int id_discount;
                decimal discount;
                string description_discount;

                public Discount(CompanyItem companyItem, decimal discount, string description_discount) : base(companyItem.getCompany(), companyItem.id_item, companyItem.code)
                {
                    this.discount = discount;
                    this.description_discount = description_discount;

                    this.Text = discount + " %";
                }
            }
        }
    }


}
