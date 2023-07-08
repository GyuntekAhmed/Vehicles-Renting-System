namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data;
    using Interfaces;
    using Web.ViewModels.Home;
    
    public class CarService : ICarService
    {
        private readonly VehicleDbContext dbContext;

        public CarService(VehicleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> AllCarsAsync()
        {
            return await dbContext
                .Cars
                .Select(c => new IndexViewModel
                {
                    Id = c.Id.ToString(),
                    Brand = c.Brand,
                    ImageUrl = c.ImageUrl,
                })
                .ToArrayAsync();
        }
    }
}
