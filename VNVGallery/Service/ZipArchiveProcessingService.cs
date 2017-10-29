using Ionic.Zip;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using VnVGallery.Data;

namespace VnVGallery.Service
{
    public class ZipArchiveProcessingService : IZipArchiveProcessingService
    {

        public IReadOnlyList<FileDetail> ProcessFiles(CloudBlobContainer container, string path)
        {
            GalleryContext context = new GalleryContext();
            List<FileDetail> fileDetails = new List<FileDetail>();
            var zip = new ZipFile(path);
            var zipExtrationPath = path.ToLowerInvariant().Replace(".zip", "");

            Directory.CreateDirectory(zipExtrationPath);
            zip.ExtractAll(zipExtrationPath, ExtractExistingFileAction.OverwriteSilently);
            var extractedFiles = Directory.GetFiles(zipExtrationPath);
            foreach (var extractedFile in extractedFiles)
            {
                var blockBlob = container.GetBlockBlobReference(Path.GetFileName(extractedFile));
                using (var fileStream = File.OpenRead(extractedFile))
                {
                    blockBlob.UploadFromStream(fileStream);
                }

                FileDetail fileDetail = new FileDetail()
                {
                    BlobUri = blockBlob.Uri.ToString(),
                    FileName = Path.GetFileName(extractedFile),
                    Extension = Path.GetExtension(extractedFile)
                };
                fileDetails.Add(fileDetail);
            }
            zip.Dispose();
            Directory.Delete(zipExtrationPath, true);
            return fileDetails;
        }
    }
}