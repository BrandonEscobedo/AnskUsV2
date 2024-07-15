using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
namespace anskus.Infrestructure.Services
{
    internal sealed class CuestionarioActivoService(ICuestionarioActivoRepository cuestionarioActivoRepository,
        ICuestionarioRepository cuestionarioRepository) : ICuestionarioActivoService
    {
        public async Task<CuestionarioActivo> ActivarCuestionario(Guid Idcuestionario, Guid IdUser)
        {
            var Cuestionario = await cuestionarioRepository.GetbyId(Idcuestionario, IdUser);
            if (Cuestionario.Estado == EstadoCuestionario.Guardado)
            {
                Cuestionario.Estado = EstadoCuestionario.Activo;
                await cuestionarioRepository.Update(Cuestionario);
            }
            var CuestionarioActivo = await cuestionarioActivoRepository.ActivarCuestionarioAsync(Idcuestionario, IdUser);
            CuestionarioActivo.Cuestionario = Cuestionario;
            return CuestionarioActivo;
        }
    }
}
