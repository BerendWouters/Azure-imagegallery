using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using VnVGallery.Data;

namespace VnVGallery.Service
{
    public interface IZipArchiveProcessingService
    {
        IReadOnlyList<FileDetail> ProcessFiles(CloudBlobContainer container, string path);
    }
}