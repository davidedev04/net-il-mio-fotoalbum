﻿using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Data
{
    public class PhotoManager
    {
        public static int CountAllPhotos()
        {
            using PhotoContext db = new PhotoContext();
            return db.Foto.Count();
        }

        public static List<Photo> GetAllPhotos()
        {
            using PhotoContext db = new PhotoContext();
            return db.Foto.ToList();
        }

        public static List<Category> GetAllCategories()
        {
            using PhotoContext db = new PhotoContext();
            return db.Category.ToList();
        }

        public static Category GetCategoryById(int id)
        {
            using PhotoContext db = new PhotoContext();
            return db.Category.FirstOrDefault(t => t.Id == id);
        }

        public static Photo GetPhoto(int id, bool includeReferences = true)
        {
            using PhotoContext db = new PhotoContext();
            if (includeReferences)
                return db.Foto.Where(x => x.Id == id).Include(p => p.Categories).FirstOrDefault();
            return db.Foto.FirstOrDefault(p => p.Id == id);
        }

        public static void InsertPhoto(Photo photo, List<string> SelectedCategories)
        {
            using PhotoContext db = new PhotoContext();
            photo.Categories = new List<Category>();

            if (SelectedCategories != null)
            {
                // Trasformiamo gli ID scelti in ingredienti da aggiungere tra i riferimenti in Pizza
                foreach (var category in SelectedCategories)
                {
                    int id = int.Parse(category);
                    // NON usiamo un GetIngredientById() perché userebbe un db context diverso
                    // e ciò causerebbe errore in fase di salvataggio - usiamo lo stesso context all'interno della stessa operazione
                    var categoryFromDb = db.Category.FirstOrDefault(x => x.Id == id);
                    if (categoryFromDb != null)
                    {
                        photo.Categories.Add(categoryFromDb);
                    }
                }
            }

            db.Foto.Add(photo);
            db.SaveChanges();
        }

        public static void Seed()
        {
            if (PhotoManager.CountAllPhotos() == 0)
            {
                PhotoManager.InsertPhoto(new Photo("foto 1", "questa é una foto scattata al mare", "", true), new(1));
                PhotoManager.InsertPhoto(new Photo("foto 2", "questa é una foto scattata in montagna", "", true), new(2));
                PhotoManager.InsertPhoto(new Photo("foto 3", "questa é una foto scattata in piscina", "", true), new(3));
                PhotoManager.InsertPhoto(new Photo("foto 4", "questa é una foto scattata a casa", "", true), new(4));
                PhotoManager.InsertPhoto(new Photo("foto 5", "questa é una foto scattata in viaggio", "", true), new(5));
            }
        }
    }
}