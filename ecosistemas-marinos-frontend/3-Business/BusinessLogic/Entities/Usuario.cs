using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BCrypt.Net;
using BusinessLogic.Exceptions.Usuario;
using BusinessLogic.InterfacesDeDominio;


namespace BusinessLogic.Entities
{
    public abstract class Usuario : IValidable,IEntity
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Contrasenia { get; set; }
        public string ContraseniaCifrada { get;  set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public DateTime FechaIngreso { get; set; }

        public Usuario()
        {
            
        }

        public void CifrarContraseña()
        {
            ContraseniaCifrada = BCrypt.Net.BCrypt.HashPassword(Contrasenia);
        }
        
        public void Validar()
        {
            ValidarAlias();
            ValidarContrasenia();
        }

        private void ValidarAlias()
        {
            if (string.IsNullOrEmpty(Alias))
                throw new AliasUsuarioException("El Alias es requerido");

            if (Alias.Length < 6)
                throw new AliasUsuarioException("El Alias debe tener un mínimo de 6 caracteres.");
        }

        private void ValidarContrasenia()
        {
            if (string.IsNullOrEmpty(Contrasenia))
                throw new ContraseniaUsuarioException("La Contraseña es requerida.");

            if (Contrasenia.Length < 8)
                throw new ContraseniaUsuarioException("La Contraseña debe tener un mínimo de 8 caracteres.");

            string validacionContrasenia = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[.,;:#¡])\S{8,}$";

            if (!Regex.IsMatch(Contrasenia, validacionContrasenia))
                throw new ContraseniaUsuarioException("La Contrasenia debe incluir al menos una mayúscula, una minúscula, un dígito y un carácter de los siguientes . , # ; : !");
        }
    }
}