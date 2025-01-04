using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yacaba.Domain.Models;
using Yacaba.EntityFramework.Identity;

namespace Yacaba.EntityFramework {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, String> {

        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Gym> Gyms { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            //optionsBuilder.AddInterceptors(_publishDomainEventInterceptor, _auditableInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //modelBuilder.UseIdentityByDefaultColumns();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            //AuditableConfiguration.Configure(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

    }
}
