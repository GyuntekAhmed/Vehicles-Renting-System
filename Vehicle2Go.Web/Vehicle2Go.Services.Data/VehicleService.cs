namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data;
    using Interfaces;
    using Web.ViewModels.Home;

    public class VehicleService : IVehicleService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public VehicleService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> AllVehiclesAsync()
        {
            var allVehicles = new List<IndexViewModel>();

            var allCars = await dbContext
                .Cars
                .Select(c => new IndexViewModel()
                {
                    Id = c.Id.ToString(),
                    Brand = c.Brand,
                    ImageUrl = c.ImageUrl,
                    VehicleType = "Car"
                })
                .ToListAsync();

            var allMotorcycles = await dbContext
                .Motorcycles
                .Select(m => new IndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Brand = m.Brand,
                    ImageUrl = m.ImageUrl,
                    VehicleType = "Motorcycle"
                })
                .ToListAsync();

            allVehicles.AddRange(allCars);
            allVehicles.AddRange(allMotorcycles);

            return allVehicles;
        }
    }
}
