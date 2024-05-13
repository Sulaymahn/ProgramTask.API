using Microsoft.EntityFrameworkCore;
using ProgramTask.API.Models;

namespace ProgramTask.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<JobProgram> JobPrograms => Set<JobProgram>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<CandidateApplication> CandidateApplications => Set<CandidateApplication>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobProgram>()
                .ToContainer(nameof(JobProgram))
                .HasPartitionKey(j => j.Id)
                .HasNoDiscriminator();

            modelBuilder.Entity<CandidateApplication>()
                .ToContainer(nameof(CandidateApplication))
                .HasPartitionKey(a => a.Id)
                .HasNoDiscriminator();
            
            modelBuilder.Entity<Question>()
                .ToContainer(nameof(Question))
                .HasPartitionKey(q => q.Id)
                .HasNoDiscriminator();
        }
    }
}
