using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class EcosistemaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Area { get; set; }
        public string Descripcion { get; set; }
        public List<PaisDto> Paises { get; set; }
        public EstadoDeConservacionDto EstadoDeConservacion { get; set; }
        public List<AmenazaDto> Amenazas { get; set; }
        public UbicacionEcosistema Ubicacion { get; set; }
        public List<ImagenEcosistema> Imagenes { get; set; }
        public List<EspecieDto> EspeciesQueLoHabitan { get; set; }
        public List<EspecieDto> EspeciesQuePuedenHabitarlo { get; set; }
    }
}
