namespace VehiclesRenting.Services
{
    using Interfaces;
    using Data;
    using Web.ViewModels.Home;

    public class MotorcycleService : IMotorcycleService
    {
        private readonly VehiclesRentingDbContext dbContext;

        public MotorcycleService(VehiclesRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<MotorcycleIndexViewModel> AllMotorcyclesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
