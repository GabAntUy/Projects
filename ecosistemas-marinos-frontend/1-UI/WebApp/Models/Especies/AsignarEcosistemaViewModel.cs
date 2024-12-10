using BusinessLogic.Entities;

namespace WebApp.Models.Especies
{
    public class AsignarEcosistemaViewModel
    {
        public string Nombre { get; set; }
        public int EspecieId { get; set; }
        public List<Ecosistema> EcosistemasPosibles { get; set; }
        public int EcosistemaId { get; set; }
        public string? EstaAsociada { get; set; }
    }
}
