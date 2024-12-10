﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces
{
    public interface IGetAllByEcosistema<T>
    {
        public IEnumerable<T> GetAllByEcosistema(string str);
    }
}
