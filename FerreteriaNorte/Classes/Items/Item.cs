using FerreteriaNorte.Classes.Brands;
using FerreteriaNorte.Classes.Companies;
using FerreteriaNorte.Classes.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Items
{

    class Item : IComparable
    {
        int id;
        string name, description;
        List<Brand> brands { get; set; }
        List<Company> companies { get; set; }
        List<Picture> pictures { get; set; }
        List<Subtitle> subtitles { get; set; }

        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        public Item()
        {
        }

        public Item(int id, string name, string description)
        {
            this.id = id;
            this.Value = id;
            this.name = name;
            this.Text = name;
            this.description = description;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Item;
            return item != null &&
                   id == item.id;
        }

        public override string ToString()
        {
            return getId().ToString() + " - " + getName();
            //return base.ToString();
        }

        public bool addBrand(Brand brand)
        {
            if (! this.brands.Contains(brand))
            {
                this.brands.Add(brand);
                return true;
            }
            return false;
        }

        public bool delBrand(Brand brand)
        {
            return this.brands.Remove(brand);
        }

        public int getId() { return this.id; }
        public void setId(int id) { this.id = id; }

        public string getName() { return this.name; }
        public void setName(string name) { this.name = name; }

        public string getDescription() { return this.description; }
        public void setDescription(string description) { this.description = description; }

        public int CompareTo(object obj)
        {
            return this.name.CompareTo(((Item)obj).name);
        }
    }
}
