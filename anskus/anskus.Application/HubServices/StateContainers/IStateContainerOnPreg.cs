using anskus.Application.DTOs;
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
        public DatosCuestionario DatosCuestionario { get; }
        public int PreguntaActual { get; }
        public void TiempoTermino();
        public void NavegarAClasificacion();
    }

}
