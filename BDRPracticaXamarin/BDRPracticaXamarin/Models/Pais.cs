using System;
using System.Collections.Generic;
using System.Text;

namespace BDRPracticaXamarin.Models
{
    public class Pais
    {
        public Int64 Id { get; set; }
        public string Nombre { get; set; }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
