using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {
        [Key] public int Id { get; set; }
        [Required(ErrorMessage = "Il campo é richiesto")]
        [StringLength(50, ErrorMessage = "Il campo deve contenere massimo 50 caratteri")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Il campo é richiesto")]
        [StringLength(300, ErrorMessage = "Il campo deve contenere masssimo 50 caratteri")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Il campo é richiesto")]
        public string Image { get; set; }
        public bool Visible { get; set; }

        public List<Category>? Categories { get; set; }

        public Photo() { }

        public Photo(string title, string description, string image, bool visible)
        {
            Title = title;
            Description = description;
            Image = image;
            Visible = visible;
        }
    }
}
