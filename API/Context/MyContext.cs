using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> option) : base(option)
        {

        }
        public DbSet<Employee> Employees { get; set; } 
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RoleAccount> RoleAccounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                 .HasOne(a => a.Account)
                 .WithOne(b => b.Employee)
                 .HasForeignKey<Account>(k => k.NIK);

             modelBuilder.Entity<Account>()
                 .HasOne(c => c.Profiling)
                 .WithOne(d => d.Account)
                 .HasForeignKey<Profiling>(k => k.NIK);

            modelBuilder.Entity<Profiling>()
                .HasOne(e => e.Education)
                .WithMany(a => a.Profiling);

            modelBuilder.Entity<Education>()
                .HasOne(e => e.University)
                .WithMany(a => a.Education);

            modelBuilder.Entity<RoleAccount>()
                .HasKey(ra => new { ra.RoleId, ra.NIK });

            modelBuilder.Entity<RoleAccount>()
                .HasOne(ra => ra.Role)
                .WithMany(r => r.RoleAccounts)
                .HasForeignKey(ra => ra.RoleId);

            modelBuilder.Entity<RoleAccount>()
                .HasOne(ra => ra.Account)
                .WithMany(a => a.RoleAccounts)
                .HasForeignKey(ra => ra.NIK);

        }
    }
}
