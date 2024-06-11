using anskus.Domain.Account;
using anskus.Domain.Models.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Account
{
    internal sealed class CreateAccountCommandHandler(IAccountRepository _accountRepository) : IRequestHandler<CreateAccountCommand, GeneralResponse>
    {
        public async Task<GeneralResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
        var response=  await  _accountRepository.CreateAccountAsync(request.CreateAccount);
            return response;
        }
    }
}
