using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class RangoConservacion
    {

        private int _minimo;
        private int _maximo;
        public int Maximo { get { return _maximo; } private set { _maximo = value; } }
        public int Minimo { get { return _minimo; } private set { _minimo = value; } }

        public RangoConservacion( int maximo, int minimo)
        {
           _minimo = minimo;
           _maximo = maximo;
        }
    }
}
