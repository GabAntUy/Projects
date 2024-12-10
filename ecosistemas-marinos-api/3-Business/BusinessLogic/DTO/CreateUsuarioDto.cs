using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class CreateUsuarioDto
    {
        public string Alias { get; set; }
        public string Contrasenia { get; set; }
        public string ContraseniaConfirmacion { get; set; }

    }
}
