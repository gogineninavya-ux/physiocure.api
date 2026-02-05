using Microsoft.EntityFrameworkCore;
using Physiocure.API.Models;

namespace Physiocure.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Admin> Admins { get; set; }   // ✅ add this
    }
}
