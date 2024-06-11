using anskus.Application.Account;
using anskus.Domain.Models.Authentication;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace anskus.WebApi.EndPoints
{
    public static class Account
    {
        public static void MapAccountEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("identity/register", async (CreateAccountDTO createAccount,
                IValidator<CreateAccountCommand> validator, 
                ISender sender) =>
            {
                CreateAccountCommand createAccountCommand = new CreateAccountCommand(createAccount);
                var result = await validator.ValidateAsync(createAccountCommand);
                if (!result.IsValid)
                {
                    return Results.ValidationProblem(result.ToDictionary());
                }
              var response=  await sender.Send(createAccountCommand);
                return Results.Ok(response);
            });
            app.MapPost("identity/Login", async (LoginDTO login, ISender sender) =>
            {
                var response = await sender.Send(new LoginAccountCommand(login));
                return Results.Ok(response);
            });
        }
    }
}
