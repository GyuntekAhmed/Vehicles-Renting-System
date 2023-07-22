namespace Vehicle2Go.Services.Data
{
    using Web.ViewModels.Vehicle;
    using Interfaces;
    using Vehicle2Go.Data;
    using Vehicle2Go.Data.Models.Vehicle;

    public class TruckService : ITruckService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public TruckService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(VehicleFormModel formModel, string agentId)
        {
            Truck newTruck = new Truck()
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

            await this.dbContext.Trucks.AddAsync(newTruck);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
