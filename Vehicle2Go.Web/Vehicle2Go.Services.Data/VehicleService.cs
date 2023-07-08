namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data;
    using Interfaces;
    using Web.ViewModels.Home;

    public class VehicleService : IVehicleService
    {
        private readonly VehicleDbContext dbContext;

        public VehicleService(VehicleDbContext dbContext)
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

            var jets = await dbContext.Jets
                .OrderBy(j => j.CreatedOn)
                .Select(j => new IndexViewModel()
                {
                    Id = j.Id.ToString(),
                    Brand = j.Brand,
                    ImageUrl = j.ImageUrl,
                })
                .ToListAsync();

            var yachts = await dbContext.Yachts
                .OrderBy(y => y.CreatedOn)
                .Select(y => new IndexViewModel()
                {
                    Id = y.Id.ToString(),
                    Brand = y.Brand,
                    ImageUrl = y.ImageUrl,
                })
                .ToListAsync();
            
            allVehicles.AddRange(allCars);
            allVehicles.AddRange(motorcycles);
            allVehicles.AddRange(jets);
            allVehicles.AddRange(yachts);

            return allVehicles;
        }
    }
}
