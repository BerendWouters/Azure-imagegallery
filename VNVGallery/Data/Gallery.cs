using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using VnVGallery.Models;

namespace VnVGallery.Data
{
    public class Gallery : EntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<FileDetail> Photos { get; set; }

        [NotMapped]
        public FileDetail RandomFile
        {
            get
            {
                var rand = new Random();
                if(Photos.Count > 0)
                    return Photos.ElementAt(rand.Next(Photos.Count()));
                return FileDetail.GetPlaceKitten();
            }
        }
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public ApplicationUser Owner { get; set; }
    }
}