using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FerreteriaNorte.Resources.Utils
{
    class CommonViewConfigurations
    {
        public static void ConfigDataGrid(DataGrid dataGrid)
        {
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
            dataGrid.SelectionUnit = DataGridSelectionUnit.FullRow;
            dataGrid.IsReadOnly = true;
        }
    }
}
