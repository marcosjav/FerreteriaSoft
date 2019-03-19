using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Extra
{
    class Title : IComparable
    {
        public int id { get; set; }
        public string name { get; set; }

        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        public Title()
        {
        }

        public Title(string name)
        {
            this.name = name;
            this.Text = name;
        }

        public Title(int id, string name)
        {
            this.id = id;
            this.name = name;
            this.Text = name;
            this.Value = id;
        }

        //public override bool Equals(object obj)
        //{
        //    var title = obj as Title;
        //    return title != null &&
        //           id == title.id;
        //}

        // This functions are used to combobox
        public override string ToString()
        {
            return Text;
        }

        public int CompareTo(object obj)
        {
            return name.CompareTo(((Title)obj).name);
        }
    }

    class Subtitle :IComparable
    {
        public int id { get; set; }
        public int title_id { get; set; }
        public string name { get; set; }
        
        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        public Subtitle()
        {
        }

        public Subtitle(int title_id, string name)
        {
            this.title_id = title_id;
            this.name = name;
            this.Text = name;
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

        public override int GetHashCode()
        {
            var hashCode = 1297908309;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + title_id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Value);
            return hashCode;
        }
    }
}
