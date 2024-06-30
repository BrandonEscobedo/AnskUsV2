using anskus.Application.ContentMultimedia.Command;
using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
using MediatR;

namespace anskus.WebApi.EndPoints
{
    public static class MediaCuestionarios
    {
        public static void MapMediaCuestionariosEndPoints(this IEndpointRouteBuilder app)
        {
            var groups = app.MapGroup("api/MediaCuestionarios");

            groups.MapPost("", async (IFormFile file, ISender sender) =>
            {
                using Stream stream = file.OpenReadStream();
                
                var response = await sender.Send(new UploadContentCommand(stream, file.ContentType));
                var result = new DatosMedia()
                {
                    IdImagen=response,
                    ContentType = file.ContentType
                };
                return Results.Ok(result);

              
            }).DisableAntiforgery().RequireAuthorization();
            groups.MapGet("/{fileId}", async (Guid fileId, IBlobService blobService) =>
            {
                FileResponse fileResponse = await blobService.DownloadAsync(fileId);
                return Results.File(fileResponse.stream, fileResponse.contentType);
            }).WithTags("Files")
        .DisableAntiforgery();
            groups.MapDelete("/{fileId}", async (Guid fileId, IBlobService blobService) =>
            {
                await blobService.DeleteAsync(fileId);
                return Results.NoContent();
            }).RequireAuthorization()
         .DisableAntiforgery();
        }
    }
}
