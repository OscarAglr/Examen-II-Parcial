using ExamenPrestamos.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenPrestamos.Model
{
    public class PrestamoModel
    {
        private List<Prestamo> prestamos = new List<Prestamo>();

        public void AddPrestamo(Prestamo p)
        {
            if (p == null)
            {
                return;
            }
            prestamos.Add(p);
        }

        public List<Prestamo> GetAll()
        {
            return prestamos;
        }

        public Prestamo GetPrestamo(int index)
        {
            if (index < 0 || index >= prestamos.Count)
            {
                return null;
            }
            if (prestamos == null)
            {
                return null;
            }
            return prestamos[index];
        }
    }
}
