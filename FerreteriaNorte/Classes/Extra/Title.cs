using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Extra
{
    class Item :IComparable
    {
        int id { get; set; }
        string name { get; set; }

        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        public Item()
        {
        }

        public Item(int id, string name)
        {
            this.id = id;
            this.name = name;
            this.Text = name;
            this.Value = id;
        }

        public override bool Equals(object obj)
        {
            var title = obj as Item;
            return title != null &&
                   id == title.id;
        }

        // This functions are used to combobox
        public override string ToString()
        {
            return Text;
        }

        public int CompareTo(object obj)
        {
            return name.CompareTo(((Item)obj).name);
        }
    }

    class Subtitle :IComparable
    {
        int id { get; set; }
        int title_id { get; set; }
        string name { get; set; }
        
        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        public Subtitle()
        {
        }

        public Subtitle(int id, int title_id, string name)
        {
            this.id = id;
            this.title_id = title_id;
            this.name = name;
            this.Text = name;
            this.Value = id;
        }

        public override bool Equals(object obj)
        {
            var subtitle = obj as Subtitle;
            return subtitle != null &&
                   id == subtitle.id &&
                   title_id == subtitle.title_id;
        }

        // This functions are used to combobox
        public override string ToString()
        {
            return Text;
        }

        public int CompareTo(object obj)
        {
            return name.CompareTo(((Subtitle)obj).name);
        }
    }
}
