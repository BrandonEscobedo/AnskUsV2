using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Cuestionarios
{
    public interface IBlobService
    {
        Task<Guid> UpdloadAync(Stream stream,string contentType,CancellationToken cancellationToken=default);
        public Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid fileId,CancellationToken cancellationToken=default);
    }
}
public record FileResponse(Stream stream, string contentType);