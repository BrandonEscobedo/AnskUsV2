using anskus.Application.Codigo;
using anskus.Application.CuestionarioActivo.ActivarCuestionario;
using anskus.Application.CuestionarioActivo.Command;
using anskus.WebApi.Hubs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace anskus.WebApi.EndPoints
{
    public static class CuestionarioActivo
    {
        public static void MapCuestionarioActivoEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/CuestionarioActivo", async (Guid idcuestionario,
                ClaimsPrincipal User, ISender sender, IValidator<ActivarCuestionarioCommand> validator) =>
            {

                var email = User.FindFirst(ClaimTypes.Email)!.Value;
                ActivarCuestionarioCommand activarCuestionarioCommand = new ActivarCuestionarioCommand(idcuestionario, email);
                var resultValid =await validator.ValidateAsync(activarCuestionarioCommand);
                if (resultValid.IsValid)
                {
                    var response = await sender.Send(activarCuestionarioCommand);
                    return Results.Ok(response);
                }
                else
                {
                    return Results.ValidationProblem(resultValid.ToDictionary());
                }

            }).RequireAuthorization();
            app.MapGet("api/CuestionarioActivo", async (int Code, IVerificarCodigo _verificarCodigo) =>
            {
                var isValid = await _verificarCodigo.ExecuteAsync(Code);
                if (isValid)
                {
                    return Results.Ok();
                }
                return Results.NotFound();
            });
            app.MapDelete("api/CuestionarioActivo/{idcuestionario}", async (Guid idcuestionario,ClaimsPrincipal User, IMediator sender)=>{
                var email = User.FindFirst(ClaimTypes.Email)!.Value;
                await sender.Send(new RemoveCuesActFromUserCommand(idcuestionario, email));
                return Results.Ok();
            }).RequireAuthorization();
            app.MapPost("api/CuestionarioActivo/Sala", async (int code, string nombre, IValidator<AddUserToRoomCommand> validator,
               ISender sender) =>
            {
                AddUserToRoomCommand addUserToRoomCommand = new(code, nombre);
                var result = await validator.ValidateAsync(addUserToRoomCommand);
                if (result.IsValid)
                {
                   var response= await sender.Send(addUserToRoomCommand);
                    return Results.Ok(response);
                }
                else
                {
                    return Results.ValidationProblem(result.ToDictionary());
                }
            });
        }
    }
}
