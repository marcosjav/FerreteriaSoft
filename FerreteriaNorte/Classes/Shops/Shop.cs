using FerreteriaNorte.Classes.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Shops
{
    class Shop : IComparable
    {
        public int id { get; set; }
        public string name { get; set; }
        public int idAddress { get; set; }
        public string address { get; set; }

        public string Text { get; set; }
        public object Value { get; set; }

        public Shop()
        {

        }

        public Shop(string name, int idAddress)
        {
            this.name = name;
            this.idAddress = idAddress;
            setValues();
        }

        public Shop(int id, string name, int idAddress)
        {
            this.id = id;
            this.name = name;
            this.idAddress = idAddress;
            setValues();
        }

        public Shop(int id, string name, int idAddress, string address) : this(id, name, idAddress)
        {
            this.address = address;
        }

        private void setValues()
        {
            this.Text = this.name;
            this.Value = this.id;
        }

        // This functions are used to combobox
        public override string ToString()
        {
            return Text + " - " + id.ToString();
        }

        public int CompareTo(object obj)
        {
            int result = this.name.CompareTo(((Shop)obj).name);
            if (result == 0)
            {
                return this.address.CompareTo(((Shop)obj).address);
            }
            return result;
        }
    }



}
