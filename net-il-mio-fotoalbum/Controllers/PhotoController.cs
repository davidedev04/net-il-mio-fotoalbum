using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;

namespace net_il_mio_fotoalbum.Controllers
{

    [Authorize(Roles = "ADMIN,USER")]
    public class PhotoController : Controller
    {

        private readonly ILogger<PhotoController> _logger;

        public PhotoController(ILogger<PhotoController> logger)
        {
            _logger = logger;
        }

        // GET: PhotoController
        [HttpGet]
        public ActionResult Index()
        {
            return View(PhotoManager.GetAllPhotos());
        }

        // GET: PhotoController/Details/5
        [HttpGet]
        public ActionResult GetPhoto(int id)
        {
            var photo = PhotoManager.GetPhoto(id);
            if (photo != null)
                return View(photo);
            else
                return View("errore");
        }

        // GET: PhotoController/Create
        [HttpGet]
        public ActionResult Create()
        {
            Photo p = new Photo();
            PhotoFormModel model = new PhotoFormModel(p);
            model.CreateCategories();
            return View(model);
        }

        // POST: PhotoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel photoC)
        {
            if (ModelState.IsValid == false)
            {
                // Ritorno la form di prima con i dati della pizza
                // precompilati dall'utente
                photoC.CreateCategories();
                return View("Create", photoC);
            }

            PhotoManager.InsertPhoto(photoC.Photo, photoC.SelectedCategories);
            // Richiamiamo la action Index affinché vengano mostrate tutte le pizze
            // inclusa quella nuova
            return RedirectToAction("Index");
        }

        // GET: PhotoController/Edit/5
        [HttpGet]
        public ActionResult Update(int id)
        {
            var photo = PhotoManager.GetPhoto(id);
            if (photo == null)
                return NotFound();
            PhotoFormModel model = new PhotoFormModel(photo);
            model.CreateCategories();
            return View(model);
        }

        // POST: PhotoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id,PhotoFormModel model)
        {
            if (ModelState.IsValid == false)
            {
                // Ritorno la form di prima con i dati della pizza
                // precompilati dall'utente
                model.CreateCategories();
                return View("Update", model);
            }

            var modified = PhotoManager.UpdatePhoto(id, model.Photo, model.SelectedCategories);
            if (modified)
            {
                // Richiamiamo la action Index affinché vengano mostrate tutte le pizze
                return RedirectToAction("Index");
            }
            else
                return NotFound();
        }

        // POST: PhotoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (PhotoContext context = new PhotoContext())
            {
                Photo photoToDelete = context.Foto.Where(photo => photo.Id == id).FirstOrDefault();

                if (photoToDelete != null)
                {
                    context.Foto.Remove(photoToDelete);

                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
