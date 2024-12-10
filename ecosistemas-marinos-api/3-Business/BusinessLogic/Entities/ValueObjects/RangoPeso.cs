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
        private int _minimo;
        private int _maximo;
        public int Max { get { return _maximo; } private set { _maximo = value; } }
        public int Min { get { return _minimo; } private set { _minimo = value; } }

        public RangoPeso(int min, int max)
        {

            _minimo = min;
            _maximo = max;
            Validar();
        }

        public void Validar()
        {
            ValidarMax();
            ValidarMin();
        }

        private void ValidarMax()
        {
            if (_maximo < 0)
                throw new RangoEspecieException("El valor no puede ser menor a cero");
        }

        private void ValidarMin()
        {
            if (_minimo < 0)
                throw new RangoEspecieException("El valor no puede ser menor a cero");
        }
    }
}
