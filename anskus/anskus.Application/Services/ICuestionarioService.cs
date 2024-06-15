using anskus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Services
{
    public interface ICuestionarioService
    {
        public Task<Cuestionario> CrearCuestionario(Cuestionario cuestionario);
        public Task<Cuestionario> ActualizarCuestionario(Cuestionario cuestionario);
        public Task EliminarCuestionario(Cuestionario cuestionario);
    }

}
