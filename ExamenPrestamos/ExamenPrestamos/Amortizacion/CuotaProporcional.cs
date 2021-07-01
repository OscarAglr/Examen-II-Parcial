using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenPrestamos.Amortizacion
{
    public class CuotaProporcional
    {
        public decimal[] CalcularAbono(decimal saldoInsoluto, decimal tasa, int numPagos)
        {
            decimal abono = saldoInsoluto / numPagos;
            return Enumerable.Repeat(abono, numPagos).ToArray();
        }

        public decimal[] CalcularCuota(decimal saldoInsoluto, decimal tasa, int numPagos)
        {
            decimal[] cuota = new decimal[numPagos];
            decimal[] abonos = CalcularAbono(saldoInsoluto, tasa, numPagos);
            decimal[] intereses = CalcularInteres(saldoInsoluto, tasa, numPagos);
            for (int i = 0; i < numPagos; i++)
            {
                cuota[i] = abonos[i] + intereses[i];
            }
            return cuota;
        }

        public decimal[] CalcularInteres(decimal saldoInsoluto, decimal tasa, int numPagos)
        {
            decimal[] intereses = new decimal[numPagos];
            decimal abono = saldoInsoluto / numPagos;
            decimal interes = saldoInsoluto * tasa;
            for (int i = 0; i < numPagos; i++)
            {
                intereses[i] = interes;
                saldoInsoluto -= abono;
                interes = saldoInsoluto * tasa;
            }
            return intereses;
        }

        public decimal[] GetSaldoInsoluto(decimal saldoInsoluto, decimal tasa, int numPagos)
        {
            decimal[] saldos = new decimal[numPagos + 1];
            decimal[] abonos = CalcularAbono(saldoInsoluto, tasa, numPagos);
            for (int i = 0; i < numPagos; i++)
            {
                saldos[i] = saldoInsoluto;
                saldoInsoluto -= abonos[i];
            }
            return saldos;
        }
    }
}
