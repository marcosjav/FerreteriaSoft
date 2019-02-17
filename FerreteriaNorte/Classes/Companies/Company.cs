﻿using FerreteriaNorte.Classes.Extra;
using FerreteriaNorte.Classes.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Companies
{
    class Company : IComparable
    {
        public int id { get; set; }
        public string name { get; set; }
        public string web { get; set; }
        public string cuit { get; set; }
        public Address address { get; set; }
        public List<Phone> phones { get; set; }
        public List<string> emails { get; set; }

        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        public Company()
        {
        }

        public Company(int id, string name, string web, string cuit)
        {
            this.id = id;
            this.Value = id;
            this.name = name;
            this.Text = name;
            this.web = web;
            this.cuit = cuit;
            setValues();
        }

        public Company(int id, string name, string web, string cuit, Address address, List<Phone> phones, List<string> emails) : this(id, name, web, cuit)
        {
            this.address = address;
            this.phones = phones;
            this.emails = emails;
        }

        public override bool Equals(object obj)
        {
            var company = obj as Company;
            return company != null &&
                   id == company.id &&
                   cuit == company.cuit;
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
            data += "    " + address.street + " " + address.number + Environment.NewLine + Environment.NewLine;
            data += "Web:" + Environment.NewLine + "    " + web + Environment.NewLine + Environment.NewLine;
            data += "Teléfonos:" + Environment.NewLine;
            foreach (Phone phone in phones)
            {
                data += "     (" + PhoneHelper.GetPhoneType(phone.type).name + ") " + phone.area + phone.number + Environment.NewLine;
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
