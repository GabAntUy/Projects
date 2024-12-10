using BusinessLogic.Entities;

namespace WebApp.Models.Ecosistemas
{
    public class IndexEcosistemaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Area { get; set; }
        public string Descripcion { get; set; }
        public List<Pais> Paises { get; set; }
        public EstadoDeConservacion EstadoDeConservacion { get; set; }
        public List<Amenaza> Amenazas { get; set; }
        public UbicacionEcosistema Ubicacion { get; set; }
        public List<ImagenEcosistema> Imagenes { get; set; }
        public List<Especie> EspeciesQueLoHabitan { get; set; }

        public IndexEcosistemaViewModel()
        {
            
        }
    }
}
