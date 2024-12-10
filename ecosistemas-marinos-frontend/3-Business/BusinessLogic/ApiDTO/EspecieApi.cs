﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApiDTO
{
    public class EspecieApi
    {
        public string NombreCientifico { get; set; }

        public string NombreVulgar { get; set; }

        public string Descripcion { get; set; }

        public IEnumerable<int> EcosistemasId { get; set; }

        public List<int> AmenazasId { get; set; }
        public int EstadoDeConservacionId { get; set; }

        public int LargoMin { get; set; }

        public int LargoMax { get; set; }

        public int PesoMin { get; set; }

        public int PesoMax { get; set; }
    }
}