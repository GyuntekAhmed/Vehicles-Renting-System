namespace Vehicle2Go.Services.Data
{
    using System.Threading.Tasks;

    using Interfaces;
    using Vehicle2Go.Data;
    using Vehicle2Go.Data.Models.Vehicle;
    using Web.ViewModels.Car;

    public class CarService : ICarService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public CarService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
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
