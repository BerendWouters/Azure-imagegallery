using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageGallery.Data
{
    public class FileDetail : EntityBase
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int GalleryId { get; set; }
        [ForeignKey("GalleryId")]
        public virtual Gallery Gallery { get; set; }

        public string BlobUri { get; set; }

        public static FileDetail GetPlaceKitten()
        {
            return new FileDetail()
            {
                BlobUri = "https://placekitten.com/200/300"
            };
        }
    }
}