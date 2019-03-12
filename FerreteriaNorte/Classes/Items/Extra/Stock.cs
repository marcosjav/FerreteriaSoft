using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Items.Extra
{
    public class Stock
    {
        public double quantity_stock { get; set; }
        public double min_stock { get; set; }
        public int unit_id { get; set; }
        public int shop_id_shop { get; set; }
        public int item_id { get; set; }

        public Stock() { }

        public Stock(double quantity_stock, double min_stock, int unit_id, int shop_id_shop, int item_id)
        {
            this.quantity_stock = quantity_stock;
            this.min_stock = min_stock;
            this.unit_id = unit_id;
            this.shop_id_shop = shop_id_shop;
            this.item_id = item_id;
        }
    }
}
