using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Items
{
    public class Discount
    {
        public int value_discount { get; set; }
        public string description_discount { get; set; }
    }

    public class Company
    {
        public int company_id { get; set; }
        public string code { get; set; }
        public double cost_item { get; set; }
        public List<Discount> discounts { get; set; }
    }

    public class Stock
    {
        public int quantity_stock { get; set; }
        public int min_stock { get; set; }
        public int unit_id { get; set; }
        public int shop_id_shop { get; set; }
    }

    public class RootObject
    {
        public string description_item { get; set; }
        public string name_item { get; set; }
        public List<int> brands { get; set; }
        public List<int> subtitles { get; set; }
        public List<Company> companies { get; set; }
        public List<Stock> stocks { get; set; }
    }
}
