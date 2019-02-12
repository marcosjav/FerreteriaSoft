using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Companies
{
    class Company : IComparable
    {
        int id { get; set; }
        string name { get; set; }
        string web { get; set; }
        string cuit { get; set; }

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

        public int CompareTo(object obj)
        {
            return name.CompareTo(((Company)obj).name);
        }

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
