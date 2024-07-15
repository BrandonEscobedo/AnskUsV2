using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Services
{
    public interface ICuestionarioActivoServices
    {
        public Task<(Guid IdCuestionario, Dictionary<string, string[]> errors)> AddUserToRoom(int Codigo, string Nombre);
        Task<Guid> ActivarCuestionario(Guid? IdCuestionario);
        Task<bool> VerificarCodigo(int Code);
        public Task EliminarCuestionarioActivoAsync(Guid? idcuestionario);
    }
}
