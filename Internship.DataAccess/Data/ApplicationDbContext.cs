using Internship.Models;
using Microsoft.EntityFrameworkCore;

namespace Internship.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
    }
        public DbSet<InternshipJob> InternshipJobs { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<InternshipJob>().HasData(
        //        new InternshipJob
        //        );
        //}
    }
}
