using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ImageGallery.Data;

namespace ImageGallery.Controllers
{
    public class HomeController : Controller
    {
        private GalleryContext db = new GalleryContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.Galleries.ToListAsync());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}