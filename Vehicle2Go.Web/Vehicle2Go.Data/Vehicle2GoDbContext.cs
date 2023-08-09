// ReSharper disable VirtualMemberCallInConstructor
namespace Vehicle2Go.Data
{
    using System.Reflection;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    using Models.Vehicle;
    using Models.Agent;
    using Models.Category;
    using Models.User;

    public class Vehicle2GoDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public Vehicle2GoDbContext(DbContextOptions<Vehicle2GoDbContext> options)
            : base(options)
        {
            if (!Database.IsRelational())
            {
                Database.EnsureCreated();
            }
        }

        public DbSet<CarCategory> CarCategories { get; set; } = null!;
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Agent> Agents { get; set; } = null!;
        public DbSet<MotorcycleCategory> MotorcycleCategories { get; set; } = null!;
        public DbSet<Motorcycle> Motorcycles { get; set; } = null!;
        public DbSet<JetCategory> JetCategories { get; set; } = null!;
        public DbSet<Jet> Jets { get; set; } = null!;
        public DbSet<YachtCategory> YachtCategories { get; set; } = null!;
        public DbSet<Yacht> Yachts { get; set; } = null!;
        public DbSet<TruckCategory> TruckCategories { get; set; } = null!;
        public DbSet<Truck> Trucks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(Vehicle2GoDbContext)) ??
                                      Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);
        }
    }
}