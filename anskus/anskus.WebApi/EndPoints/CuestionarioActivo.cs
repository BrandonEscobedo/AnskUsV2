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
                ClaimsPrincipal User, ISender sender) =>
            {             
                var email = User.FindFirst(ClaimTypes.Email)!.Value;
                var response = await sender.Send(new ActivarCuestionarioCommand(idcuestionario, email));
                return Results.Ok(response);
            }).RequireAuthorization();
            app.MapGet("api/CuestionarioActivo", async (int Code, IVerificarCodigo _verificarCodigo) =>
            {
                var isValid =await _verificarCodigo.ExecuteAsync(Code);
                if (isValid)
                {
                    return Results.Ok();
                }
                return Results.NotFound();
            });
            app.MapPost("api/CuestionarioActivo/Codigo", async (int code, string Nombre, IValidator<AddUserToRoomCommand> validator,
               ISender sender )=>
            {
                AddUserToRoomCommand addUserToRoomCommand = new(code,Nombre);
                var result = await validator.ValidateAsync(addUserToRoomCommand);
                if (result.IsValid)
                {
                    var resultCommand = await sender.Send(addUserToRoomCommand);
                    return Results.Ok(resultCommand);
                }
                else
                {
                    return Results.ValidationProblem(result.ToDictionary());
                }

            });
        }
    }
}
