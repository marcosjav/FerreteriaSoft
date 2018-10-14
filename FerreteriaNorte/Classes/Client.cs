using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes
{
    class Client
    {
        public int Id { get; set; }
        public int Documento { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public string CodigoPostal { get; set; }
        public string Condiciones { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Limite { get; set; }
        public string Lista { get; set; }
        public Image Foto { get; set; }
        public DateTime FechaInicio { get; set; }
    }
}
