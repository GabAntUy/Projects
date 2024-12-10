using BusinessLogic.Configuration;
using BusinessLogic.Exceptions.Ecosistema;
using BusinessLogic.InterfacesDeDominio;

namespace BusinessLogic.Entities
{
    public class Ecosistema : IValidable, IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Area { get; set; }
        public string Descripcion { get; set; }
        public bool EstaActivo { get; set; } = true;
        public List<Pais> Paises { get; set; }
        public EstadoDeConservacion EstadoDeConservacion { get; set; }
        public List<Amenaza> Amenazas { get; set; }
        public UbicacionEcosistema Ubicacion { get; set; }
        public List<ImagenEcosistema> Imagenes { get; set; }
        public List<Especie> EspeciesQueLoHabitan { get; set; }
        public List<Especie> EspeciesQuePuedenHabitarlo { get; set; }

        public void Validar()
        {
            ValidarNombre();
            ValidarArea();
            ValidarDescripcion();
            ValidarUbicacion();
            ValidarEstadoConservacion();
            //ValidarEspecies();
            ValidarAmenazas();
            ValidarPaises();
            //ValidarImagenes();
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new NombreEcosistemaException("El Nombre es requerido");
            }
            if (Nombre.Length < Config.NombreMinimo || Nombre.Length > Config.NombreMaximo)
            {
                throw new NombreEcosistemaException($"El Nombre debe tener entre {Config.NombreMinimo} y {Config.NombreMaximo}  caracteres.");
            }
        }

        private void ValidarArea()
        {
            if (Area < 0)
                throw new AreaEcosistemaException("El Area no puede tener valor negativo");
        }

        private void ValidarDescripcion()
        {
            if (string.IsNullOrEmpty(Descripcion))
                throw new DescripcionEcosistemaException("La Descripcion es requerida");

            if (Descripcion.Length < Config.DescripcionMinimo || Descripcion.Length > Config.DescripcionMaximo)
                throw new DescripcionEcosistemaException($"La Descripcion debe tener entre {Config.DescripcionMinimo} y {Config.DescripcionMaximo}  caracteres.");
        }

        private void ValidarUbicacion()
        {
            if (Ubicacion == null)
                throw new UbicacionEcosistemaException("La Ubicacion no puede ser null");
            Ubicacion.Validar();
        }

        private void ValidarEstadoConservacion()
        {
            if (EstadoDeConservacion == null)
                throw new EstadoConservacionEcosistemaException("El EstadoConservacion no puede ser null");
        }

        private void ValidarEspecies()
        {
            if (!EspeciesQueLoHabitan.Any())
                throw new EspeciesEcosistemaException("El Ecosistema debe contar con al menos 1 Especie");
        }

        private void ValidarAmenazas()
        {
            if (!Amenazas.Any())
                throw new AmenazasEcosistemaException("El Ecosistema debe contar con al menos una Amenaza");
        }

        private void ValidarPaises()
        {
            if (!Paises.Any())
                throw new PaisesEcosistemaException("El Ecosistema debe contar con al menos un Pais");
        }

        private void ValidarImagenes()
        {
            if (!Imagenes.Any())
                throw new ImagenesEcosistemaException("El Ecosistema debe contar con al menos una Imagen");
        }


        public void Update(Ecosistema eco)
        {
            Validar();
            Nombre = eco.Nombre;
            Area = eco.Area;
            Descripcion = eco.Descripcion;
            Paises = eco.Paises;
            EstadoDeConservacion = eco.EstadoDeConservacion;
            Amenazas = eco.Amenazas;
            Ubicacion = eco.Ubicacion;
            Imagenes = eco.Imagenes;
        }
    }
}
