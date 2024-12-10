using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Exceptions.Ubicacion;
using BusinessLogic.InterfacesDeDominio;


namespace BusinessLogic.Entities
{
    public class UbicacionEcosistema: IValidable
    {

        private decimal _latitud;
        private decimal _longitud;
        public decimal Latitud 
        { 
            get { return _latitud; }
            private set { _latitud = value; } 
        }

        public decimal Longitud
        {
            get { return _longitud; }
            private set { _longitud = value; }
        }
        public UbicacionEcosistema(decimal latitud, decimal longitud)
        {

            _latitud=latitud;
            _longitud=longitud;
            Validar();
        }
        public void Validar()
        {
            ValidarLatitud();
            ValidarLongitud();
        }

        private void ValidarLatitud()
        {
            if (_latitud < -90 || _latitud > 90)
                throw new LatitudUbicacionException("La Latitud debe estar entre -90 y 90.");
        }

        private void ValidarLongitud()
        {
            if (_longitud < -180 || _longitud > 180)
                throw new LongitudUbicacionException("La Longitud debe estar entre -180 y 180.");
        }

    }

}
