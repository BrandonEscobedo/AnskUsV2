using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Infrestructure.Services
{
    internal sealed class CuestionarioActivoService(ICuestionarioActivoRepository cuestionarioActivoRepository,
        ICuestionarioRepository cuestionarioRepository) : ICuestionarioActivoService
    {
        public async Task<CuestionarioActivo> ActivarCuestionario(Guid Idcuestionario, Guid IdUser)
        {
            var Cuestionario = await cuestionarioRepository.GetbyId(Idcuestionario, IdUser);
            var CuestionarioActivo = await cuestionarioActivoRepository.ActivarCuestionarioAsync(Idcuestionario, IdUser);
            CuestionarioActivo.Cuestionario = Cuestionario;
            return CuestionarioActivo;
        }
 
    }
}
