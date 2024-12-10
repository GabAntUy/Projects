using BusinessLogic.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using WebApp.Validaciones;

namespace WebApp.Models.Especies
{
    public class CreateEspecieViewModel
    {
        [Required]
        public string NombreCientifico { get; set; }
        [Required]
        public string NombreVulgar { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public IEnumerable<Ecosistema> PuedeHabitar { get; set; }
        public IEnumerable<Amenaza> Amenazas { get; set; }
        [Required]
        public IEnumerable<int> AmenazaIDs { get; set; }
        [Required]
        public IEnumerable<int> EcosistemaIDs { get; set; }
        public IEnumerable<EstadoDeConservacion> EstadosDeConservacion { get; set; }
        [Required]
        public int LargoMin { get; set; }
        [Required]
        public int LargoMax { get; set; }
        [Required]
        public int PesoMin { get; set; }
        [Required]
        public int PesoMax { get; set; }
        [Required]
        public int EstadoDeConservacionId { get; set; }

        [Required]
        [ValidarExtensionesFotos(".jpg,.jpeg,.png", ErrorMessage = "Solo se permiten archivos con extensión .jpg, .jpeg o .png.")]
        public List<IFormFile> ImagenesEspecies { get; set; }
    }
}
