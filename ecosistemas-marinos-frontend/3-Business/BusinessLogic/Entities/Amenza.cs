using BusinessLogic.Configuration;
using BusinessLogic.Exceptions.Amenaza;
using BusinessLogic.InterfacesDeDominio;


namespace BusinessLogic.Entities
{
    public class Amenaza : IValidable
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int GradoDePeligro { get; set; }
        public List<Ecosistema> Ecosistemas { get; set; }
        public List<Especie> Especies { get; set; }

        public void Validar()
        {
            ValidarDescripcion();
            ValidarGradoDePeligro();
        }
        private void ValidarDescripcion()
        {
            
            if (string.IsNullOrEmpty(Descripcion))
                throw new DescripcionAmenazaException("La Descripcion es requerida");

            if (Descripcion.Length >= Config.DescripcionMinimo || Descripcion.Length <= Config.DescripcionMaximo)
                throw new DescripcionAmenazaException($"La Descripcion debe tener entre {Config.DescripcionMinimo} y {Config.DescripcionMaximo}  caracteres.");
        }

        private void ValidarGradoDePeligro()
        {
            if (GradoDePeligro >= 1 || GradoDePeligro <= 10)
                throw new Exception("Grado De Peligro debe estar entre 1 y 10.");
        }
    }
}
