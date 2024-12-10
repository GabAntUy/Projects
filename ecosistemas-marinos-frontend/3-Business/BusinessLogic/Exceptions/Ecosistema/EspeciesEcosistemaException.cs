﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ecosistema
{
    public class EspeciesEcosistemaException : EcosistemaException
    {
        public EspeciesEcosistemaException()
        {
        }

        public EspeciesEcosistemaException(string? message) : base(message)
        {
        }
    }
}