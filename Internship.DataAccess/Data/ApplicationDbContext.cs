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
        public DbSet<Report> Reports { get; set; }
        public DbSet<InternshipCoordinator_User> Coordinators { get; set; }
        public DbSet<CareerCenter_User> CareerCenters { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<HealthInsurance> HealthInsurances { get; set; }
        public DbSet<SavedInternship> SavedInternships { get; set; }
        public DbSet<OfficialLetter> OfficialLetters { get; set; }
        public DbSet<ApplicationForm> ApplicationForms { get; set; }
        public DbSet<submittedApplicationForms> submittedApplicationForms { get; set; }
        
        
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

            modelBuilder.Entity<Student_User>()
      .HasOne(s => s.ApplicationForm)
      .WithOne(a => a.Student)
      .HasForeignKey<ApplicationForm>(a => a.StudentId)
      .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OfficialLetter>()
          .HasOne(s => s.Student)
          .WithMany()
          .HasForeignKey(s => s.StudentId)
          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HealthInsurance>()
          .HasOne(s => s.Student)
          .WithMany()
          .HasForeignKey(s => s.StudentId)
          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Report>()
          .HasOne(s => s.Student)
          .WithMany()
          .HasForeignKey(s => s.StudentId)
          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<submittedApplicationForms>()
          .HasOne(s => s.Student)
          .WithMany()
          .HasForeignKey(s => s.StudentId)
          .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
