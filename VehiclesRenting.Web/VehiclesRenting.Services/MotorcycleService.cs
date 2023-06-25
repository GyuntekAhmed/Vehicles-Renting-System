using Microsoft.EntityFrameworkCore;

namespace VehiclesRenting.Services
{
    using Interfaces;
    using Data;
    using Web.ViewModels.Home;

    public class MotorcycleService : IMotorcycleService
    {
        private readonly VehiclesRentingDbContext dbContext;

        public MotorcycleService(VehiclesRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> AllMotorcyclesAsync()
        {
            IEnumerable<MotorcycleIndexViewModel> allMotorcycles = await dbContext
                .Motorcycles
                .Select(m => new MotorcycleIndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Brand = m.Brand,
                    ImageUrl = m.ImageUrl,
                })
                .ToArrayAsync();

            return allMotorcycles;
        }
    }
}
