using anskus.Application.CuestionarioActivo.ActivarCuestionario;
using anskus.WebApi.Hubs;
using MediatR;
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
        }
    }
}
