using FerreteriaNorte.Resources.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Brands
{
    class Brand : IComparable
    {
        public int id { get; set; }
        public string name { get; set; }
        
        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        public Brand()
        {
        }

        public Brand(string name)
        {
            this.name = name;
        }

        public Brand(int id, string name)
        {
            this.id = id;
            this.name = name;
            this.Text = name;
            this.Value = id;
        }

        public override bool Equals(object obj)
        {
            var brand = obj as Brand;
            return brand != null &&
                   id == brand.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }


        // This functions are used to combobox
        public override string ToString()
        {
            return Text;
        }

        public int CompareTo(object obj)
        {
            return name.CompareTo(((Brand)obj).name);
        }

        public int sendToDB()
        {
            if (this.name != null && this.id == 0)
            {
                return BrandHelper.sendToDB(this);
            }

            return 0;
        }
    }
}
