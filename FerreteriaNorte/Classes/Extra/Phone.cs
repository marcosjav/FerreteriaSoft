using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Extra
{
    class Phone : IComparable
    {
        public int id { get; set; }
        public string number { get; set; }
        public string area { get; set; }
        public int type { get; set; }
        public string phone_type { get; set; }

        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        private void setValues()
        {
            this.Text = this.ToString();
            this.Value = this.id;
        }
        public Phone()
        {

        }

        public Phone(string number, string area, int type)
        {
            this.number = number;
            this.area = area;
            this.type = type;
            setValues();
        }

        public Phone(int id, string number, string area, int type)
        {
            this.id = id;
            this.number = number;
            this.area = area;
            this.type = type;
            setValues();
        }

        public Phone(string number, string area, int type, string phone_type) : this(number, area, type)
        {
            this.phone_type = phone_type;
        }

        public override string ToString()
        {
            return this.area + "-" + this.number + " (" + phone_type + ")";
        }

        public int CompareTo(object obj)
        {
            return this.number.CompareTo(((Phone)obj).number);
        }
    }

    class PhoneType : IComparable, IEquatable<PhoneType>
    {
        public int id { get; set; }
        public string name { get; set; }

        public PhoneType()
        {

        }

        public PhoneType(string name)
        {
            this.name = name;
        }

        public PhoneType(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }

        public int CompareTo(object obj)
        {
            return this.name.CompareTo(((PhoneType)obj).name);
        }

        public bool Equals(PhoneType other)
        {
            return this.id == other.id;
        }
    }
}
