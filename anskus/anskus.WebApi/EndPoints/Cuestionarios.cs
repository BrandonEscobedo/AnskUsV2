using anskus.Application.Cuestionarios.Commands.Create;
using anskus.Application.Cuestionarios.Commands.Update;
using anskus.Application.Cuestionarios.Querys.GetCuestionarioById;
using anskus.Domain.Models;
using MediatR;

namespace anskus.WebApi.EndPoints
{
    public static class Cuestionarios
    {
        public static void MapCuestionariosEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/Cuestionarios",async (Cuestionario cuestionario, ISender sender) =>
            {
              await  sender.Send(new CreateCuestionarioCommand(cuestionario));
                return Results.Ok();
            });
            app.MapGet("api/Cuestionarios", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetCuestionarioByIdQuery(id));
                return Results.Ok(result);
            });
            app.MapPut("api/Cuestionarios", async (Cuestionario cuestionario, ISender sender) =>
            {
                await sender.Send(new UpdateCuestionarioCommand(cuestionario));
                return Results.Ok();
            });
        }
    }
}
