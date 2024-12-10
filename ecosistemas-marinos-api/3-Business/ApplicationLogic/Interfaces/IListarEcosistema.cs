﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces
{
    public interface IListarEcosistema<T>
    {
        public IEnumerable<T> EcosistemasQueNoHabitaLaEspecie(int id);

    }
}
