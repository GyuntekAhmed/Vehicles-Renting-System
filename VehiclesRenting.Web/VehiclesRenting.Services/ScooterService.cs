namespace VehiclesRenting.Services
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using Data;
    using Web.ViewModels.Home;

    public class ScooterService : IScooterService
    {
        private readonly VehiclesRentingDbContext dbContext;

        public ScooterService(VehiclesRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> AllScootersAsync()
        {
            IEnumerable<IndexViewModel> assScooters = await dbContext.Scooters
                .Select(s => new IndexViewModel()
                {
                    Id = s.Id.ToString(),
                    Brand = s.Brand,
                    ImageUrl = s.ImageUrl,
                })
                .ToArrayAsync();

            return assScooters;
        }
    }
}
