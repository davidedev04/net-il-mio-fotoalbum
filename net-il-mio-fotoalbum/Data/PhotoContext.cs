using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Data
{
    public class PhotoContext : DbContext
    {
        public DbSet<Photo> Foto {  get; set; }
        public DbSet<Category> Category { get; set; }

        public const string CONNECTION_STRING = "Data Source=localhost;Initial Catalog=albumfotografico;Integrated Security=True;TrustServerCertificate=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
        }
    }
}
