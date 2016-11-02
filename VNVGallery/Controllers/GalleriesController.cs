using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ionic.Zip;
using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using VnVGallery.Data;

namespace VnVGallery.Controllers
{
    [Authorize]
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

        [Authorize]
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
                var userId = User.Identity.GetUserId();
                var fileDetails = UploadFiles(gallery.Name);
                gallery.Photos = fileDetails;
                gallery.OwnerId = userId;
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
                        ConfigurationManager.AppSettings["StorageConnectionString"]);
                    var client = storageAccount.CreateCloudBlobClient();
                    var containerName = galleryName.Replace(" ", string.Empty).ToLowerInvariant();
                    CloudBlobContainer container = client.GetContainerReference(containerName);
                    container.CreateIfNotExists();
                    container.SetPermissions(
                        new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessType.Blob});
                    

                    Directory.CreateDirectory(Server.MapPath("~/App_Data/Upload"));
                     
                    var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileName);
                    file.SaveAs(path);
                    var extension = Path.GetExtension(file.FileName);
                    if (extension != null && extension.ToLowerInvariant().Equals(".zip"))
                    {
                        var zip = new ZipFile(path);
                        var zipExtrationPath = path.Replace(extension, "");

                        Directory.CreateDirectory(zipExtrationPath);
                        zip.ExtractAll(zipExtrationPath, ExtractExistingFileAction.OverwriteSilently);
                        var extractedFiles = Directory.GetFiles(zipExtrationPath);
                        foreach (var extractedFile in extractedFiles)
                        {
                            var blockBlob = container.GetBlockBlobReference(extractedFile);
                            using (var fileStream = System.IO.File.OpenRead(extractedFile))
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
                    }
                    else
                    {
                        var blockBlob = container.GetBlockBlobReference(fileName);
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
                    
                    System.IO.File.Delete(path);
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
            if (gallery.OwnerId != User.Identity.GetUserId())
            {
                return RedirectToAction("Login", "Account");
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
                if (dbGallery.OwnerId != User.Identity.GetUserId())
                {
                    return RedirectToAction("Login", "Account");
                }
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
