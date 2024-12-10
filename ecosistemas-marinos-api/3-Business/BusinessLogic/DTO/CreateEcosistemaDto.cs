using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class CreateEcosistemaDto
    {
        public string Nombre { get; set; }
        public double Area { get; set; }
        public string Descripcion { get; set; }
        public int EstadoDeConservacionId { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public IEnumerable<int> PaisesId { get; set; }
        public List<int> AmenazasId { get; set; }
    }
}
