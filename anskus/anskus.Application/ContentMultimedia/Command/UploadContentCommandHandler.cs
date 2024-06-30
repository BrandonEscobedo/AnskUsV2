using anskus.Domain.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.ContentMultimedia.Command
{
    internal sealed class UploadContentCommandHandler(IBlobService _blobService) : IRequestHandler<UploadContentCommand, Guid>
    {
        public async Task<Guid> Handle(UploadContentCommand request, CancellationToken cancellationToken)
        {
            var response = await _blobService.UpdloadAync(request.File, request.ContentType, cancellationToken);
            return response;    
        }
    }
}
