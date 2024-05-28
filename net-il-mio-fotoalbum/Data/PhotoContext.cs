using net_il_mio_fotoalbum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Data
{
    public class PhotoContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Photo> Foto {  get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Message> Message { get; set; }

        public const string CONNECTION_STRING = "Data Source=localhost;Initial Catalog=albumfotografico;Integrated Security=True;TrustServerCertificate=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
        }
    }
}
