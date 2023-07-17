namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Vehicle2Go.Data;
    using Interfaces;
    using Web.ViewModels.Home;
    using Vehicle2Go.Data.Models.Vehicle;
    using Web.ViewModels.Car;

    public class CarService : ICarService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public CarService(Vehicle2GoDbContext dbContext)
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

        public async Task CreateAsync(CarFormModel formModel, string agentId)
        {
            Car newCar = new Car
            {
                Brand = formModel.Brand,
                Model = formModel.Model,
                RegistrationNumber = formModel.RegistrationNumber,
                Address = formModel.Address,
                PricePerDay = formModel.PricePerDay,
                Color = formModel.Color,
                ImageUrl = formModel.ImageUrl,
                CategoryId = formModel.CategoryId,
                AgentId = Guid.Parse(agentId),
            };

            await dbContext.Cars.AddAsync(newCar);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
