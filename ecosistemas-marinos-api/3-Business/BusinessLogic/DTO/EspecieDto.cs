using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class EspecieDto
    {
        public int Id { get; set; }
        public string NombreCientifico { get; set; }
        public string NombreVulgar { get; set; }
        public string Descripcion { get; set; }
        public List<EcosistemaDto> PuedeHabitar { get; set; }
        public EcosistemaDto? Habita { get; set; }
        //public List<ImagenEspecie> Imagenes { get; set; }
        public RangoPeso RangoPeso { get; set; }
        public RangoLargo RangoLargo { get; set; }
        public List<AmenazaDto> Amenazas { get; set; }
        public EstadoDeConservacionDto EstadoConservacion { get; set; }
    }
}
