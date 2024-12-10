﻿using BusinessLogic.Configuration;
using BusinessLogic.Entities.ValueObjects.Generic;
using BusinessLogic.Exceptions.EstadoConservacion;
using BusinessLogic.Exceptions.Pais;
using BusinessLogic.InterfacesDeDominio;


namespace BusinessLogic.Entities
{
    public class EstadoDeConservacion :  IEntity
    {
        public int Id { get; set; }
        public Nombre Nombre { get; set; }
        public RangoConservacion RangoConservacion { get; set; }

        //public void Validar()
        //{
        //    ValidarNombre();
        //}

    //    private void ValidarNombre()
    //    {
    //        if (string.IsNullOrEmpty(Nombre))
    //            throw new NombreEstadoConservacionException ("El Nombre es requerido");

    //        if (Nombre.Length < Config.NombreMinimo || Nombre.Length > Config.NombreMaximo)
    //            throw new NombreEstadoConservacionException($"El Nombre debe tener entre {Config.NombreMinimo} y {Config.NombreMaximo}  caracteres.");
    //}
}
}

