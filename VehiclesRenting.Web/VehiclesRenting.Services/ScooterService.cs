namespace VehiclesRenting.Services
{
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

        public Task<IEnumerable<ScooterIndexViewModel>> AllScootersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
