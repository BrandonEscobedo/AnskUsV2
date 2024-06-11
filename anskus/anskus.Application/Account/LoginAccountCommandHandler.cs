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
    internal sealed class LoginAccountCommandHandler(IAccountRepository _accountRepository) : IRequestHandler<LoginAccountCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            var response= await _accountRepository.LoginAccountAsync(request.Login);
            return response;
        }
    }
}
