using Microsoft.EntityFrameworkCore;

using MyFirstWebAPI.Models;

namespace MyFirstWebAPI.Классы
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
    }
}
