namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data;
    using Interfaces;
    using Web.ViewModels.Home;

    public class MotorcycleService : IMotorcycleService
    {
        private readonly VehicleDbContext dbContext;

        public MotorcycleService(VehicleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> AllMotorcyclesAsync()
        {
            return await dbContext
                .Motorcycles
                .Select(m => new IndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Brand = m.Brand,
                    ImageUrl = m.ImageUrl,
                })
                .ToArrayAsync();
        }
    }
}
