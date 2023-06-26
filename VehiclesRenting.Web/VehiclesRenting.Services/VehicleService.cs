namespace VehiclesRenting.Services
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using Data;
    using Web.ViewModels.Home;

    public class VehicleService : IVehicleService
    {
        private readonly VehiclesRentingDbContext dbContext;

        public VehicleService(VehiclesRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<IndexViewModel>> AllVehiclesAsync()
        {
            List<IndexViewModel> allVehicles = new List<IndexViewModel>();

            var allCars = await dbContext.Cars
                .OrderBy(c => c.CreatedOn)
                .Select(c => new IndexViewModel()
                {
                    Id = c.Id.ToString(),
                    Brand = c.Brand,
                    ImageUrl = c.ImageUrl,
                })
                .ToListAsync();

            var motorcycles = await dbContext.Motorcycles
                .OrderBy(m => m.CreatedOn)
                .Select(m => new IndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Brand = m.Brand,
                    ImageUrl = m.ImageUrl,
                })
                .ToListAsync();

            var scooters = await dbContext.Scooters
                .OrderBy(s => s.CreatedOn)
                .Select(s => new IndexViewModel()
                {
                    Id = s.Id.ToString(),
                    Brand = s.Brand,
                    ImageUrl = s.ImageUrl,
                })
                .ToListAsync();

            allVehicles.AddRange(allCars);
            allVehicles.AddRange(motorcycles);
            allVehicles.AddRange(scooters);

            return allVehicles;
        }
    }
}
