using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Usuarios
{
    public class CreateUsuarioViewModel
    {
        [Required]
        public string Alias { get; set; }
        [Required]
        public string Contrasenia1 { get; set; }
        [Required]
        public string Contrasenia2 { get; set; }
    }
}
