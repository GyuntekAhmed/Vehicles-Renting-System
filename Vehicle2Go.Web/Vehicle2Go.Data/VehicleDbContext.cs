using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Vehicle2Go.Data.Models.Category;

namespace Vehicle2Go.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class VehicleDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options)
            : base(options)
        {
        }

        public DbSet<CarCategory> CarCategories { get; set; } = null!;
        public DbSet<MotorcycleCategory> MotorcycleCategories { get; set; } = null!;
        public DbSet<JetCategory> JetCategories { get; set; } = null!;
        public DbSet<YachtCategory> YachtCategories { get; set; } = null!;
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Motorcycle> Motorcycles { get; set; } = null!;
        public DbSet<Jet> Jets { get; set; } = null!;
        public DbSet<Yacht> Yachts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(VehicleDbContext)) ??
                                      Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);
        }
    }
}