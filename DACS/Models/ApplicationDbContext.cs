using Microsoft.EntityFrameworkCore;

namespace DACS.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
        }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<RoomCategories> Rooms { get; set; }

    }
}
