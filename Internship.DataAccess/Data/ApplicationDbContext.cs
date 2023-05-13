using Internship.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Internship.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
    }
        public DbSet<InternshipOpportunity> InternshipOpportunities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Student_User> Students { get; set; }
        public DbSet<InternshipCoordinator_User> Coordinators { get; set; }
        public DbSet<CareerCenter_User> CareerCenters { get; set; }
        public DbSet<Admin_User> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configure foreign key relationships
            modelBuilder.Entity<Student_User>()
            .Property(s => s.StudentId)
            .HasDefaultValueSql("NEXT VALUE FOR dbo.StudentIDSeq");
            modelBuilder.Entity<Student_User>()
              .HasOne(s => s.User)
              .WithOne()
               .HasForeignKey<Student_User>(s => s.UserId);

            modelBuilder.Entity<Student_User>()
                .HasOne(s => s.Coordinator)
                .WithMany()
                .HasForeignKey(s => s.CoordinatorId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<Admin_User>()
                .HasOne(cc => cc.User)
                .WithOne()
                .HasForeignKey<Admin_User>(cc => cc.UserId);

            modelBuilder.Entity<InternshipCoordinator_User>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<InternshipCoordinator_User>(c => c.UserId);

            modelBuilder.Entity<CareerCenter_User>()
                .HasOne(cc => cc.User)
                .WithOne()
                .HasForeignKey<CareerCenter_User>(cc => cc.UserId);
        }
    }
}
