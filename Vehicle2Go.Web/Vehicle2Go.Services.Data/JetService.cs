namespace Vehicle2Go.Services.Data
{
    using Web.ViewModels.Vehicle;
    using Interfaces;
    using Vehicle2Go.Data;
    using Vehicle2Go.Data.Models.Vehicle;

    public class JetService : IJetService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public JetService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(VehicleFormModel formModel, string agentId)
        {
            Jet newJet = new Jet()
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

            await this.dbContext.Jets.AddAsync(newJet);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
