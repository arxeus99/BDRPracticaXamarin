using System;
using System.Collections.Generic;
using System.Text;

namespace BDRPracticaXamarin.Models
{
    public class Cliente
    {
        public Int64 Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Poblacion { get; set; }
        public Int64 IdPais { get; set; }
        public string NombrePais { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
