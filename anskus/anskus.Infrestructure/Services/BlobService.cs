using anskus.Domain.Cuestionarios;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Infrestructure.Services
{
    internal sealed class BlobService(BlobServiceClient _blobServiceClient) : IBlobService
    {
        private const string ContainerName = "files";
        public async Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default)
        {
            BlobContainerClient ContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = ContainerClient.GetBlobClient(fileId.ToString());
            await blobClient.DeleteIfExistsAsync(cancellationToken:cancellationToken);
        }

        public async Task<FileResponse> DownloadAsync(Guid fileId,  CancellationToken cancellationToken = default)
        {
            BlobContainerClient ContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = ContainerClient.GetBlobClient(fileId.ToString());
          Response<  BlobDownloadResult> response = await blobClient.DownloadContentAsync(cancellationToken:cancellationToken);
            return new FileResponse(response.Value.Content.ToStream(), response.Value.Details.ContentType);
        }

        public async Task<Guid> UpdloadAync(Stream stream, string contentType, CancellationToken cancellationToken = default)
        {
            BlobContainerClient ContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            var fileId = Guid.NewGuid();
            BlobClient blobClient = ContainerClient.GetBlobClient(fileId.ToString());
            await blobClient.UploadAsync(stream,
                new BlobHttpHeaders { ContentType = contentType, }, cancellationToken: cancellationToken);
            return fileId;
        }
    }
}
