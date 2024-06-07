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
        public DbSet<SupportRequest> SupportRequests { get; set; }
        public DbSet<RoomSchedule> RoomSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.RoomCategory)
                .WithMany(rc => rc.Classes)
                .HasForeignKey(c => c.MaLoaiPhong);

            modelBuilder.Entity<SupportRequest>()
    .HasOne(sr => sr.Class)
    .WithMany()
    .HasForeignKey(sr => sr.MaPhongHoc)
    .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<SupportRequest>()
                .HasOne(sr => sr.IssueCategory)
                .WithMany()
                .HasForeignKey(sr => sr.IssueCategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<SupportRequest>()
                .HasOne(sr => sr.IssueDetail)
                .WithMany()
                .HasForeignKey(sr => sr.IssueDetailId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<SupportRequest>()
                .HasOne(sr => sr.ApplicationUser)
                .WithMany()
                .HasForeignKey(sr => sr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
