using anskus.Application.CuestionarioActivo.Command;
using anskus.Domain.Cuestionarios;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo.Validator
{
    public sealed class IngresarASalaValidator: AbstractValidator<AddUserToRoomCommand>
    {
        public IngresarASalaValidator(ICuestionarioActivoRepository _cuestionarioActivoRepository)
        {
            
            RuleFor(x => x.Name  ).MustAsync(async (Command, Name, _) =>
            {
                return await _cuestionarioActivoRepository.IsParticipanteUniqueAsync(Command.Code,Command.Name);
            }).WithMessage("Este nombre no esta disponible");

        }
    }
}
