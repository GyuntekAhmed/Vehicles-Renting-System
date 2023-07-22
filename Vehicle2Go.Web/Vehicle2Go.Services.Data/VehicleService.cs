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

            var allJets = await dbContext
                .Jets
                .Select(j => new IndexViewModel()
                {
                    Id = j.Id.ToString(),
                    Brand = j.Brand,
                    ImageUrl = j.ImageUrl,
                    VehicleType = "Jet"
                })
                .ToListAsync();

            var allYachts = await dbContext
                .Yachts
                .Select(y => new IndexViewModel()
                {
                    Id = y.Id.ToString(),
                    Brand = y.Brand,
                    ImageUrl = y.ImageUrl,
                    VehicleType = "Yacht"
                })
                .ToListAsync();

            var allTrucks = await dbContext
                .Trucks
                .Select(t => new IndexViewModel()
                {
                    Id = t.Id.ToString(),
                    Brand = t.Brand,
                    ImageUrl = t.ImageUrl,
                    VehicleType = "Truck"
                })
                .ToListAsync();

            allVehicles.AddRange(allCars);
            allVehicles.AddRange(allMotorcycles);
            allVehicles.AddRange(allJets);
            allVehicles.AddRange(allYachts);
            allVehicles.AddRange(allTrucks);

            return allVehicles;
        }
    }
}
