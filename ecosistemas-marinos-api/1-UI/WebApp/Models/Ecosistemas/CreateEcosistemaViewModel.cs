using BusinessLogic.Entities;
using System.ComponentModel.DataAnnotations;
using WebApp.Validaciones;

namespace WebApp.Models.Ecosistemas
{
    public class CreateEcosistemaViewModel
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public double Area { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int EstadoDeConservacionId { get; set; }
        [Required]
        public decimal Longitud { get; set; }
        [Required]
        public decimal Latitud { get; set; }
        public IEnumerable<Pais>? Paises { get; set; }
        [Required]
        public IEnumerable<int> PaisesId { get; set; }
        public IEnumerable<EstadoDeConservacion>? EstadoDeConservacion { get; set; }
        public IEnumerable<Amenaza>? Amenazas { get; set; }
        [Required]
        public List<int> AmenazasId { get; set; }
        [Required]
        [ValidarExtensionesFotos(".jpg,.jpeg,.png", ErrorMessage = "Solo se permiten archivos con extensión .jpg, .jpeg o .png.")]
        public List<IFormFile> ImagenesEcosistema { get; set; }
    }
}
