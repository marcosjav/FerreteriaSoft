using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes
{
    class Provider
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public int CUIT { get; set; }
        public string Direccion { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public string CodigoPostal { get; set; }
        public int IdRubro { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaInicio { get; set; }
        public string IVA { get; set; }
        public string Web { get; set; }
        public int IdCondicionVenta { get; set; }
    }
}
