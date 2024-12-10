﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions.Ecosistema
{
    public class EcosistemaException : DomainException
    {
        public EcosistemaException(string? message) : base(message)
        {
        }
        public EcosistemaException()
        {
            
        }
    }
}