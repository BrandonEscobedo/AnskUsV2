using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using anskus.Domain.Account;
using FluentValidation;
namespace anskus.Application.Account.Validator
{
    public sealed class CreateAccountCommandValidator:AbstractValidator<CreateAccountCommand >
    {
        public CreateAccountCommandValidator(IAccountRepository accountRepository)
        {
            RuleFor(x => x.CreateAccount.Email)
                .NotEmpty().WithMessage("El campo correo no puede estar vacio");

            RuleFor(x => x.CreateAccount.ConfirmPassword)
                .Equal(x => x.CreateAccount.Password)
                .WithMessage("El campo contraseña y confirmar contraseña deben ser iguales");
            RuleFor(x => x.CreateAccount.Password)
                .NotEmpty().WithMessage("El campo contraseña no puede estar vacio");

            RuleFor(c => c.CreateAccount.Email).MustAsync(async (email, _) =>
            {
                return await accountRepository.isEmailUniqueAsync(email);
            }).WithMessage("El correo ya esta en uso");
        }

    }
}
