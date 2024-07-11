using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.HubServices.StateContainers
{
    public interface IStateContainerOnPreg : IStateBase
    {
        public event Action? OnNavegarAClasificacion;
        public event Action? OnTiempoTermino;
        public void TiempoTermino();
        public void NavegarAClasificacion();
    }

}
