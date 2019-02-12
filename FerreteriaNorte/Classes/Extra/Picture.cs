using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes
{
    class Picture
    {
        int id { get; set; }
        Image picture { get; set; }
        

        public Picture()
        {
        }

        public Picture(int id, Image picture)
        {
            this.id = id;
            this.picture = picture;
        }
    }
}
