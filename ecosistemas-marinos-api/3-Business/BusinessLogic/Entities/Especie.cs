using BusinessLogic.Configuration;
using BusinessLogic.Entities.ValueObjects.Generic;
using BusinessLogic.Exceptions.Especie;
using BusinessLogic.Exceptions.EstadoConservacion;
using BusinessLogic.InterfacesDeDominio;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace BusinessLogic.Entities
{
    public class Especie: IValidable, IEntity
    {
        public int Id { get; set; }
        public Nombre NombreCientifico { get; set; }
        public Nombre NombreVulgar { get; set; }
        public string Descripcion { get;set; }

        public List<Ecosistema> PuedeHabitar { get; set; }
        public Ecosistema? Habita { get; set; }
        public List<ImagenEspecie> Imagenes { get; set; }
        public RangoPeso RangoPeso { get; set; }
        public RangoLargo RangoLargo {  get; set; }
        public List<Amenaza> Amenazas { get; set; }
        public EstadoDeConservacion  EstadoConservacion { get; set; }

        public void Validar()
        {
            //ValidarNombreCientifico();
            //ValidarNombreVulgar();
            ValidarDescripcion();
            ValidarRangoPeso();
            ValidarRangoLargo();
            ValidarEstadoConservacion();
            ValidarAmenazas();
           // ValidarImagenes();
            //ValidarHabita();
            ValidarPuedeHabitar();
        }

        private void ValidarHabita()
        {
            if (Habita == null)
                throw new EspecieException("El Ecositema que habita es requerido.");
        }
        private void ValidarPuedeHabitar()
        {
            if (!PuedeHabitar.Any())
                throw new EspecieException("Los Ecositemas que puede habitar son requeridas.");
        }
        private void ValidarImagenes()
        {
            if (!Imagenes.Any())
                throw new EspecieException("Las Imagenes son requeridas.");
        }
        private void ValidarAmenazas()
        {
            if (!Amenazas.Any())
                throw new EspecieException("Las amenazas son requeridas.");
        }
        //private void ValidarNombreCientifico()
        //{
        //    if (string.IsNullOrEmpty(NombreCientifico))
        //        throw new NombreCientificoEspecieException("El Nombre es requerido");

        //    if (NombreCientifico.Length <= Config.NombreMinimo || NombreCientifico.Length >= Config.NombreMaximo)
        //        throw new NombreCientificoEspecieException($"El Nombre Cientifico debe tener entre {Config.NombreMinimo} y {Config.NombreMaximo}  caracteres.");
        //}
        //private void ValidarNombreVulgar()
        //{
        //    if (string.IsNullOrEmpty(NombreVulgar))
        //        throw new NombreVulgarEspecieException("El Nombre es requerido");

        //    if (NombreVulgar.Length <= Config.NombreMinimo || NombreVulgar.Length >= Config.NombreMaximo)
        //        throw new NombreVulgarEspecieException($"El Nombre vulgar debe tener entre {Config.NombreMinimo} y {Config.NombreMaximo}  caracteres.");
        //}
        private void ValidarDescripcion()
        {
            if (string.IsNullOrEmpty(Descripcion))
                throw new NombreVulgarEspecieException("La descripcion es requerida");

            if (Descripcion.Length <= Config.DescripcionMinimo || Descripcion.Length >= Config.DescripcionMaximo)
                throw new DescripcionEspecieException($"La Descripcion debe tener entre {Config.DescripcionMinimo} y {Config.DescripcionMaximo}  caracteres.");
        }
        private void ValidarRangoPeso()
        {
            RangoPeso.Validar();
        }
        private void ValidarRangoLargo()
        {
            RangoLargo.Validar();
        }
        private void ValidarEstadoConservacion()
        {
            if (EstadoConservacion == null)
                throw new EspecieException("El Estado de conservacion no puede ser nulo");
        }

        public void Update(Especie especie)
        {

            Validar();
            NombreCientifico = especie.NombreCientifico;
            NombreVulgar = especie.NombreVulgar;
            Descripcion = especie.Descripcion;
            PuedeHabitar = especie.PuedeHabitar;
            Habita = especie.Habita;
            Imagenes = especie.Imagenes;
            RangoPeso = especie.RangoPeso;
            RangoLargo = especie.RangoLargo;
            Amenazas = especie.Amenazas;
            EstadoConservacion = especie.EstadoConservacion;
            /**/
        }
    }
}
