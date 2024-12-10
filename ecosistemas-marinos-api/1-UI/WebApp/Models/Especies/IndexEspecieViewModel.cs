using BusinessLogic.Entities;

namespace WebApp.Models.Especies
{
    public class IndexEspecieViewModel
    {
        public int Id { get; set; }
        public string NombreCientifico { get; set; }
        public string NombreVulgar { get; set; }
        public string Descripcion { get; set; }
        public List<Ecosistema> PuedeHabitar { get; set; }
        public Ecosistema? Habita { get; set; }
        public List<ImagenEspecie> Imagenes { get; set; }
        public RangoPeso RangoPeso { get; set; }
        public RangoLargo RangoLargo { get; set; }
        public List<Amenaza> Amenazas { get; set; }
        public EstadoDeConservacion EstadoConservacion { get; set; }
    }
}
