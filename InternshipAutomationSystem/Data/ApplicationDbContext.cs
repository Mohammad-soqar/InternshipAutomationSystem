using InternshipAutomationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipAutomationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
    }
        public DbSet<InternshipJob> InternshipJobs { get; set; }
    }
}
