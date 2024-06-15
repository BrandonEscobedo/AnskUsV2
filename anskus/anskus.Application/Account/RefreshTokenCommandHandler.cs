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
    internal sealed class RefreshTokenCommandHandler(IAccountRepository accountRepository) : IRequestHandler<RefreshTokenCommand, LoginResponse>
    {

        public async Task<LoginResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {

            var response = await accountRepository.RefreshTokenAsync(request.RefreshToken);
            return response;
        }
    }
}
