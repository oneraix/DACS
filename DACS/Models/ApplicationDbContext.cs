using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DACS.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<RoomCategories> RoomCategories { get; set; }
        public DbSet<LoanEquipment> LoanEquipments { get; set; }
        public DbSet<LoanEquipmentTicket> LoanEquipmentTickets { get; set; }
        public DbSet<IssueCategory> IssueCategories { get; set; }
        public DbSet<IssueDetail> IssueDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.RoomCategory)
                .WithMany(rc => rc.Classes)
                .HasForeignKey(c => c.MaLoaiPhong);
        }

    }
}
