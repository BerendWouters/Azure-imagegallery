using System.Collections.Generic;
using System.Linq;
using VnVGallery.Data;

namespace VnVGallery.Models
{
    public class AccountModel
    {
        public static AccountModel Create(ApplicationUser user, IQueryable<Gallery> galleries)
        {
            return new AccountModel()
            {
                _user = user,
                Galleries = galleries.ToList()
            };
        }

        private ApplicationUser _user { get; set; }

        public List<Gallery> Galleries { get; set; }
        public string Email { get { return _user.Email; } }
    }
}