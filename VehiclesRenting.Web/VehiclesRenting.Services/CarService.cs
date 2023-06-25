namespace VehiclesRenting.Services
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using Data;
    using Web.ViewModels.Home;

    public class CarService : ICarService
    {
        private readonly VehiclesRentingDbContext dbContext;

        public CarService(VehiclesRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<IndexViewModel>> AllCarsAsync()
        {
            IEnumerable<IndexViewModel> allCars = await dbContext.Cars
                .Select(c => new IndexViewModel()
                {
                    Id = c.Id.ToString(),
                    Brand = c.Brand,
                    ImageUrl = c.ImageUrl,
                })
                .ToListAsync();

            return allCars;
        }
    }
}
