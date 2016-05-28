using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImageGallery.Data;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure; // Namespace for Azure Configuration Manager
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage

namespace ImageGallery.Controllers
{
    public class GalleriesController : Controller
    {
        private GalleryContext db = new GalleryContext();


        // GET: Galleries
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            return View(await db.Galleries.ToListAsync());
        }


        // GET: Galleries/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = await db.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // GET: Galleries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                var fileDetails = UploadFiles(gallery.Name);
                gallery.Photos = fileDetails;
                db.Galleries.Add(gallery);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        private List<FileDetail> UploadFiles(string galleryName)
        {
            List<FileDetail> fileDetails = new List<FileDetail>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);


                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                        CloudConfigurationManager.GetSetting("StorageConnectionString"));
                    var client = storageAccount.CreateCloudBlobClient();
                    var containerName = galleryName.Replace(" ", string.Empty).ToLowerInvariant();
                    CloudBlobContainer container = client.GetContainerReference(containerName);
                    container.CreateIfNotExists();
                    container.SetPermissions(
                        new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessType.Blob});
                    var blockBlob = container.GetBlockBlobReference(fileName);

                    Directory.CreateDirectory(Server.MapPath("~/App_Data/Upload"));
                     
                    var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileName);
                    file.SaveAs(path);
                    using (var fileStream = System.IO.File.OpenRead(path))
                    {
                        blockBlob.UploadFromStream(fileStream);
                    }

                    FileDetail fileDetail = new FileDetail()
                    {
                        BlobUri = blockBlob.Uri.ToString(),
                        FileName = fileName,
                        Extension = Path.GetExtension(fileName)
                    };
                    fileDetails.Add(fileDetail);
                }
            }
            return fileDetails;
        }

        // GET: Galleries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = await db.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                var dbGallery = db.Galleries.FindAsync(gallery.Id).Result;
                var galleryPhotos = dbGallery.Photos.ToList();
                var fileDetails = UploadFiles(dbGallery.Name);
                galleryPhotos.AddRange(fileDetails);
                dbGallery.Photos = galleryPhotos;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        // GET: Galleries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = await db.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Gallery gallery = await db.Galleries.FindAsync(id);
            db.Galleries.Remove(gallery);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
