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
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set;}

        public void Validar()
        {
            ValidarLatitud();
            ValidarLongitud();
        }

        private void ValidarLatitud()
        {
            if (Latitud < -90 || Latitud > 90)
                throw new LatitudUbicacionException("La Latitud debe estar entre -90 y 90.");
        }

        private void ValidarLongitud()
        {
            if (Longitud < -180 || Longitud > 180)
                throw new LongitudUbicacionException("La Longitud debe estar entre -180 y 180.");
        }

    }

}
