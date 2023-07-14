namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
                })
                .ToListAsync();

            allVehicles.AddRange(allCars);

            return allVehicles;
        }
    }
}
