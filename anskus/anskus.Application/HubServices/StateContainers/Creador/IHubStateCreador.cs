using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.HubServices.StateContainers.Creador
{
    public interface IHubStateCreador
    {
        public CuestionarioResponse Cuestionario { get; set; }
        public int Codigo { get; set; }
        public void SetCuestionario(CuestionarioResponse Cuestionario, int Codigo);
        public Pregunta MandarSiguientePregunta();

    }
}
