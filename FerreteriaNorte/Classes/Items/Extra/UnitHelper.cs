using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FerreteriaNorte.Classes.Items.Extra
{
    class UnitHelper
    {
        /// <summary>
        /// Parse a Unit JSONObject to a Unit class object
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Returns NULL if JSONObject don't contains id and name of unit</returns>
        public static Unit parseUnit(string str)
        {
            JObject jsonUnit = JObject.Parse(str);
            string json_id = jsonUnit[DBKeys.Unit.ID].ToString();

            int id = 0;
            string name = jsonUnit[DBKeys.Unit.NAME].ToString();

            if (int.TryParse(json_id, out id) && name != null)
            {
                return new Unit(id, name);
            }

            return null;
        }

        /// <summary>
        /// Call to DB API REST to get the Unit list
        /// </summary>
        /// <returns>A list of Unit objects</returns>
        public static List<Unit> GetUnits()
        {
            List<Unit> units = new List<Unit>();

            string request = Properties.Resources.base_url + "unit/list";

            string response = Functions.readRequest(request);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                Unit newUnit = parseUnit(str);
                if (newUnit != null)
                {
                    units.Add(newUnit);
                }
            }

            units.Sort();

            return units;
        }

        /// <summary>
        /// Load the list of units into a DataGridView
        /// </summary>
        /// <param name="datagrid">The DataGridView object</param>
        public static void setUnitGrid(DataGrid datagrid, List<Unit> units = null)
        {
            if (units == null)
                units = GetUnits();

            datagrid.ItemsSource = units;

            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            datagrid.IsReadOnly = true;

            foreach (DataGridColumn item in datagrid.Columns)
            {
                switch ((string)item.Header)
                {
                    case "Value":
                        item.Header = "Código";
                        item.DisplayIndex = 0;
                        break;
                    case "Text":
                        item.Header = "Nombre";
                        item.DisplayIndex = 1;
                        break;
                    default:
                        item.Visibility = System.Windows.Visibility.Hidden;
                        break;
                }
            }

        }

        /// <summary>
        /// Custom insert of Unit List in DataGrid
        /// </summary>
        /// <param name="datagrid">DataGrid Object</param>
        public static void LoadUnitsGrid(DataGrid datagrid)
        {
            List<Unit> units = GetUnits();
            DataTable dt = new DataTable();

            CommonViewConfigurations.ConfigDataGrid(datagrid);

            // first add your columns
            dt.Columns.Add("Código");
            dt.Columns.Add("Unidad");

            foreach (Unit item in units)
            {
                var row = dt.NewRow();
                // Set values for columns with row[i] = xy
                row[0] = item.id_unit.ToString("D5");
                row[1] = item.name_unit;

                dt.Rows.Add(row);
            }

            datagrid.DataContext = dt;
        }

        public static int sendToDB(Unit unit)
        {
            string request = Properties.Resources.base_url + "unit/add";

            request += "?name=" + unit.name_unit;

            string response = Functions.readRequest(request);

            if (response != null)
            {
                return int.Parse(response);
            }

            return 0;
        }
    }
}
