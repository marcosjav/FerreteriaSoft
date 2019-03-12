using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Items.Extra
{
    class Unit : IComparable
    {
        public int id_unit { get; set; }
        public string name_unit { get; set; }

        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        public Unit()
        {

        }

        public Unit(string name_unit)
        {
            this.name_unit = name_unit;
        }

        public Unit(int id_unit, string name_unit)
        {
            this.id_unit = id_unit;
            this.name_unit = name_unit;
        }
        /*  END OF CTOR's  */

        public override bool Equals(object obj)
        {
            var unit = obj as Unit;
            return unit != null &&
                   id_unit == unit.id_unit;
        }

        public override int GetHashCode()
        {
            return 1777310944 + id_unit.GetHashCode();
        }


        // This functions are used to combobox
        public override string ToString()
        {
            return Text;
        }

        public int CompareTo(object obj)
        {
            return name_unit.CompareTo(((Unit)obj).name_unit);
        }

        public int sendToDB()
        {
            if (this.name_unit != null && this.id_unit == 0)
            {
                return UnitHelper.sendToDB(this);
            }

            return 0;
        }
    }
}
