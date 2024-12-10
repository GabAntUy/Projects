using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
