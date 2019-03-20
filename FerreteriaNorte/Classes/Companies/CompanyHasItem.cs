using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Companies
{
    public class CompanyHasItem
    {
        int company_id { get; set; }
        int item_id { get; set; }
        string code { get; set; }
        double cost_item { get; set; }

        public CompanyHasItem()
        {
        }

        public CompanyHasItem(int company_id, string code, double cost_item)
        {
            this.company_id = company_id;
            this.code = code;
            this.cost_item = cost_item;
        }

        public CompanyHasItem(int company_id, int item_id, string code, double cost_item)
        {
            this.company_id = company_id;
            this.item_id = item_id;
            this.code = code;
            this.cost_item = cost_item;
        }
    }
}
