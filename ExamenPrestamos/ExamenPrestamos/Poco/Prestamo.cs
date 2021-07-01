using ExamenPrestamos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenPrestamos.Poco
{
    public class Prestamo
    {
        public decimal Monto { get; set; }
        public int Plazo { get; set; }
        public Periodos Periodo { get; set; }
        public decimal Tasa { get; set; }
    }
}
