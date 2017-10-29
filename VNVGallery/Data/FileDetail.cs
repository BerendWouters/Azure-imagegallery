using System.ComponentModel.DataAnnotations.Schema;

namespace VnVGallery.Data
{
    public class FileDetail : EntityBase
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int GalleryId { get; set; }
        [ForeignKey("GalleryId")]
        public virtual Gallery Gallery { get; set; }

        public string BlobUri { get; set; }

        public static FileDetail GetPlaceHolder()
        {
            return new FileDetail()
            {
                BlobUri = "http://via.placeholder.com/200x300"
            };
        }
    }
}