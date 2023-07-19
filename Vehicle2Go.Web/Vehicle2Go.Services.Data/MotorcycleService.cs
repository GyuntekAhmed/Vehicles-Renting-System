namespace Vehicle2Go.Services.Data
{
    using Vehicle2Go.Data.Models.Vehicle;
    using Interfaces;
    using Web.ViewModels.Vehicle;
    using Vehicle2Go.Data;

    public class MotorcycleService : IMotorcycleService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public MotorcycleService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(VehicleFormModel formModel, string agentId)
        {
            Motorcycle newMotorcycle = new Motorcycle()
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

            await this.dbContext.Motorcycles.AddAsync(newMotorcycle);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
