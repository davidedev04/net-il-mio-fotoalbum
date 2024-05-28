using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotoWebController : ControllerBase
    {

        [HttpGet("{name?}")]
        public IActionResult GetAllPhotos(string? name = "") // .../GetAllPizzas/margherita
        {
            if (string.IsNullOrWhiteSpace(name))
                return Ok(PhotoManager.GetAllPhotos());
            return Ok(PhotoManager.GetPhotosByName(name));
        }

        [HttpGet]
        public IActionResult GetPhotoById(int id) // QUERY PARAM https://.../api/pizzawebapi/getpizzabyid?id=1
        {
            var photo = PhotoManager.GetPhoto(id);
            if (photo == null)
                return NotFound("Foto non trovata!");
            return Ok(photo);
        }

        [HttpGet("{name}")]
        public IActionResult GetPhotoByName(string name) // PATH PARAM https://.../api/Postwebapi/getPostbyTitle/post1
        {
            var Photo = PhotoManager.GetPhotosByName(name);
            if (Photo == null)
                return NotFound("ERRORE");
            return Ok(Photo);
        }

        [HttpPost]
        public IActionResult CreatePhoto([FromBody] Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            PhotoManager.InsertPhoto(photo, null); // pizza.Ingredients.Select(x => x.Id.ToString()).ToList());
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePhoto(int id, [FromBody] Photo photo)
        {
            var oldPhoto = PhotoManager.GetPhoto(id);
            if (oldPhoto == null)
                return NotFound("ERRORE");
            PhotoManager.UpdatePhoto(id, photo, null);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePhoto(int id)
        {
            bool found = PhotoManager.DeletePhoto(id);
            if (found)
                return Ok();
            return NotFound();
        }

    }
}
