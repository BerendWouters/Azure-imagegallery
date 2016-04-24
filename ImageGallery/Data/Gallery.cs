using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ImageGallery.Data
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
    }
}