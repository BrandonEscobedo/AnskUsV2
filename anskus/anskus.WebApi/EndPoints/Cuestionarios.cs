using anskus.Application.Cuestionarios.Commands.Create;
using anskus.Application.Cuestionarios.Commands.Update;
using anskus.Application.Cuestionarios.Querys.GetCuestionarioById;
using anskus.Application.Cuestionarios.Querys.GetCuestionarioByUser;
using anskus.Domain.Models;
using MediatR;
using System.Security.Claims;

namespace anskus.WebApi.EndPoints
{
    public static class Cuestionarios
    {
        public static void MapCuestionariosEndPoints(this IEndpointRouteBuilder app)
        {
            var groups = app.MapGroup("api/Cuestionarios").RequireAuthorization();
            groups.MapPost("", async (Cuestionario cuestionario, ISender sender, ClaimsPrincipal user) =>
            {

                var email = user.FindFirst(ClaimTypes.Email)?.Value;
                if (email == null)
                    throw new Exception("No se encontro el correo");
                await sender.Send(new CreateCuestionarioCommand(cuestionario, email));
                return Results.Ok();
            });
            groups.MapGet("", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetCuestionarioByIdQuery(id));
                return Results.Ok(result);
            });
            groups.MapGet("/All", async (ISender sender, ClaimsPrincipal User) =>
            {
                var email = User.FindFirst(ClaimTypes.Email)!.Value;
                var result = await sender.Send(new GetAllCuestionarioByUser(email));
                return Results.Ok(result);
            });
            groups.MapPut("", async (Cuestionario cuestionario, ISender sender) =>
            {
                await sender.Send(new UpdateCuestionarioCommand(cuestionario));
                return Results.Ok();
            });
        }
    }
}
