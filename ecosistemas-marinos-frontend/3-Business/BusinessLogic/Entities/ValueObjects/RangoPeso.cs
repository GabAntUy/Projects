using BusinessLogic.Exceptions.Especie;
using BusinessLogic.InterfacesDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class RangoPeso :IValidable
    {
        public int Max { get; set; }
        public int Min { get; set; }

        public void Validar()
        {
            ValidarMax();
            ValidarMin();
        }

        private void ValidarMax()
        {
            if (Max < 0)
                throw new RangoEspecieException("El valor no puede ser menor a cero");
        }

        private void ValidarMin()
        {
            if (Min < 0)
                throw new RangoEspecieException("El valor no puede ser menor a cero");
        }
    }
}
