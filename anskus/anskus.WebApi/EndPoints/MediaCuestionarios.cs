using anskus.Application.ContentMultimedia.Command;
using anskus.Domain.Cuestionarios;
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
                return Results.Ok(response);
              
            }).DisableAntiforgery().RequireAuthorization();
            groups.MapGet("/{fileId}", async (Guid fileId, IBlobService blobService) =>
            {
                FileResponse fileResponse = await blobService.DownloadAsync(fileId);
                return Results.File(fileResponse.stream, fileResponse.contentType);
            }).WithTags("Files")
        .DisableAntiforgery();
        }
    }
}
