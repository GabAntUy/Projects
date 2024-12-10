using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Interfaces
{
    public interface IAddEcosistema<T>
    {
        public void AddEcosistema(Ecosistema eco, Especie esp);
    }
}
