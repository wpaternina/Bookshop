using Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistences
{
    public class BookshopContext : IdentityDbContext<Usuario>
    {
        public BookshopContext(DbContextOptions options) : base(options) { }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
