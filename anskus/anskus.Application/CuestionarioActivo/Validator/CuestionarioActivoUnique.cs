using anskus.Application.CuestionarioActivo.ActivarCuestionario;
using anskus.Domain.Cuestionarios;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo.Validator
{
    public sealed class CuestionarioActivoUnique:AbstractValidator<ActivarCuestionarioCommand>
    {
        public CuestionarioActivoUnique(ICuestionarioActivoRepository _cuestionarioActivoRepository)
        {
            RuleFor(x => x.email).MustAsync(async (email, _) =>
            {
                return await _cuestionarioActivoRepository.IsCuestionarioActivoUnique(email);
            }).WithMessage("Ya tienes un cuestionario Activo ¿deseas eliminarlo para activar uno nuevo?");
        }
    }
}
