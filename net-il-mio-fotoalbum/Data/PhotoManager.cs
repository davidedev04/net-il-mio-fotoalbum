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

        public static void InsertPhoto(Photo photo)
        {
            using PhotoContext db = new PhotoContext();
            db.Foto.Add(photo);
            db.SaveChanges();
        }

        public static void Seed()
        {
            if (PhotoManager.CountAllPhotos() == 0)
            {
                PhotoManager.InsertPhoto(new Photo("foto 1", "questa é una foto scattata al mare", "", true));
                PhotoManager.InsertPhoto(new Photo("foto 2", "questa é una foto scattata in montagna", "", true));
                PhotoManager.InsertPhoto(new Photo("foto 3", "questa é una foto scattata in piscina", "", true));
                PhotoManager.InsertPhoto(new Photo("foto 4", "questa é una foto scattata a casa", "", true));
                PhotoManager.InsertPhoto(new Photo("foto 5", "questa é una foto scattata in viaggio", "", true));
            }
        }
    }
}
