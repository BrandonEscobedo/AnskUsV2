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
        public async Task<CuestionarioActivo> ActivarCuestionario(Guid Idcuestionario, string email)
        {
            var Cuestionario = await cuestionarioRepository.GetbyId(Idcuestionario, email);
            var CuestionarioActivo = await cuestionarioActivoRepository.ActivarCuestionarioAsync(Idcuestionario, email);
            CuestionarioActivo.Cuestionario = Cuestionario;
            return CuestionarioActivo;
        }
        
    }
}
