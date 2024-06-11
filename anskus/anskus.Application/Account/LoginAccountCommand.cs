using anskus.Domain.Models.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Account
{
    public record LoginAccountCommand(LoginDTO Login):IRequest<LoginResponse>;
}
