using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace anskus.Application.ContentMultimedia.Command
{
    public record UploadContentCommand(Stream File, string ContentType) :IRequest<Guid>;
}
