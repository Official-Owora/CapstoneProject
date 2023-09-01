using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Infrastructure.Persistence
{
    public class DataContext : IdentityDbContext<User>
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
        DbSet<User> users { get; set; }
        DbSet<Mentee> mentors { get; set; }
        DbSet<Mentee> mentees { get; set; }
        DbSet<AppointmentSchedule> appointments { get; set; }
    }
}
