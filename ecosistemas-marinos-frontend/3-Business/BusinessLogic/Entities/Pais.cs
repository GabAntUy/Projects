using BusinessLogic.Configuration;
using BusinessLogic.Exceptions.Pais;
using BusinessLogic.InterfacesDeDominio;


namespace BusinessLogic.Entities
{
    public class Pais: IValidable, IEntity
    {
        public int Id { get; set; }
        public string CodigoIso { get; set; }
        public string Nombre { get; set; }
        public List<Ecosistema> Ecosistemas { get; set; }

        public void Validar()
        {
            ValidarNombre();
            ValidarCodigo();
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
                throw new NombrePaisException("El Nombre es requerido");

            if (Nombre.Length < Config.NombreMinimo || Nombre.Length > Config.NombreMaximo)
                throw new NombrePaisException($"El Nombre debe tener entre {Config.NombreMinimo} y {Config.NombreMaximo}  caracteres.");
        }

        private void ValidarCodigo()
        {
            if (string.IsNullOrEmpty(CodigoIso))
            {
                throw new CodigoPaisException("El Codigo es requerido");
            }
        }

  
    }
}
