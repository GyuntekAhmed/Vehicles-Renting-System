namespace VehiclesRenting.Services
{
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

        public Task<IEnumerable<CarIndexViewModel>> AllCarsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
