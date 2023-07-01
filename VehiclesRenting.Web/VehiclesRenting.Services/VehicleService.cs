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
                    CategoryName = c.Category.Name,
                })
                .ToListAsync();

            var motorcycles = await dbContext.Motorcycles
                .OrderBy(m => m.CreatedOn)
                .Select(m => new IndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Brand = m.Brand,
                    ImageUrl = m.ImageUrl,
                    CategoryName = m.Category.Name,
                })
                .ToListAsync();

            var scooters = await dbContext.Scooters
                .OrderBy(s => s.CreatedOn)
                .Select(s => new IndexViewModel()
                {
                    Id = s.Id.ToString(),
                    Brand = s.Brand,
                    ImageUrl = s.ImageUrl,
                    CategoryName = s.Category.Name,
                })
                .ToListAsync();

            var jets = await dbContext.Jets
                .OrderBy(j => j.CreatedOn)
                .Select(j => new IndexViewModel()
                {
                    Id = j.Id.ToString(),
                    Brand = j.Brand,
                    ImageUrl = j.ImageUrl,
                    CategoryName = j.Category.Name,
                })
                .ToListAsync();

            var yachts = await dbContext.Yachts
                .OrderBy(y => y.CreatedOn)
                .Select(y => new IndexViewModel()
                {
                    Id = y.Id.ToString(),
                    Brand = y.Brand,
                    ImageUrl = y.ImageUrl,
                    CategoryName = y.Category.Name,
                })
                .ToListAsync();

            allVehicles.AddRange(allCars);
            allVehicles.AddRange(motorcycles);
            allVehicles.AddRange(scooters);
            allVehicles.AddRange(jets);
            allVehicles.AddRange(yachts);

            return allVehicles;
        }
    }
}
