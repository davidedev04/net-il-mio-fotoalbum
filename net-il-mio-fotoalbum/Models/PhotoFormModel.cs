using Microsoft.AspNetCore.Mvc.Rendering;
using net_il_mio_fotoalbum.Data;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoFormModel
    {
        public Photo Photo {  get; set; }

        public List<SelectListItem>? categories { get; set; }
        public List<string>? SelectedCategories { get; set; }

        public PhotoFormModel() { }

        public PhotoFormModel(Photo photo)
        {
            Photo = photo;
        }

        public void CreateCategories()
        {
            categories = new List<SelectListItem>();
            SelectedCategories = new List<string>();
            var categoriesFromDB = PhotoManager.GetAllCategories();
            foreach (var categoria in categoriesFromDB)
            {
                bool isSelected = Photo.Categories?.Any(i => i.Id == categoria.Id) == true;
                categories.Add(new SelectListItem() // lista degli elementi selezionabili
                {
                    Text = categoria.Name, // Testo visualizzato
                    Value = categoria.Id.ToString(), // SelectListItem vuole una generica stringa, non un int
                    Selected = isSelected // es. tag1, tag5, tag9
                });
                if (isSelected)
                    SelectedCategories.Add(categoria.Id.ToString()); // lista degli elementi selezionati
            }
        }
    }
}
