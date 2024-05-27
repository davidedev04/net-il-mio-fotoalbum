using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;

namespace net_il_mio_fotoalbum.Controllers
{
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
            return View();
        }

        // POST: PhotoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: PhotoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
