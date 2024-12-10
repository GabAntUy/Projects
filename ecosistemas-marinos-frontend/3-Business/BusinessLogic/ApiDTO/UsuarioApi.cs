using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApiDTO
{
    public class UsuarioApi
    {
        public string Alias { get; set; }
        public string Contrasenia { get; set; }
        public string ContraseniaConfirmacion { get; set; }
    }
}
