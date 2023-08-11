using Vehicle2Go.Web.ViewModels.Agent;

namespace Vehicle2Go.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using Web.ViewModels.Vehicle;
    using Vehicle2Go.Data;
    using Data;
    using Data.Interfaces;
    using Web.ViewModels.Vehicle.Enums;

    using static SeederDb;

    public class CarServiceTests
    {
        private DbContextOptions<Vehicle2GoDbContext> dbOptions;
        private Vehicle2GoDbContext dbContext;
        private ICarService carService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            this.dbOptions = new DbContextOptionsBuilder<Vehicle2GoDbContext>()
                .UseInMemoryDatabase
                    ("Vehicle2GoInMemory" + Guid.NewGuid().ToString()).Options;

            this.dbContext = new Vehicle2GoDbContext(dbOptions);
            this.dbContext.Database.EnsureCreated();

            SeedDatabase(this.dbContext);

            this.carService = new CarService(this.dbContext);
        }

        [Test]
        public async Task CreateAndReturnIdAsync_ShouldCreateCorrectly()
        {
            var formModel = new VehicleFormModel
            {
                Brand = Car.Brand,
                Model = Car.Model,
                RegistrationNumber = Car.RegistrationNumber,
                Address = Car.Address,
                PricePerDay = Car.PricePerDay,
                ImageUrl = Car.ImageUrl,
                Color = Car.Color,
                CategoryId = Car.CategoryId,
            };

            var agentId = AgentUser.Id.ToString();

            var result = await carService.CreateAndReturnIdAsync(formModel, agentId);

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.ToString());
        }

        [Test]
        public async Task AllAsync_ReturnsFilteredAndPagedData()
        {
            // Arrange
            var queryModel = new AllVehiclesQueryModel
            {
                Category = "SomeCategory",
                SearchString = "SomeSearch",
                VehicleSorting = VehicleSorting.Newest,
                CurrentPage = 1,
                VehiclesPerPage = 10,
                TotalVehicles = 1,
                Vehicles = new List<VehicleAllViewModel>()
                {
                    new VehicleAllViewModel
                    {
                        Id = Car.Id.ToString(),
                        Brand = Car.Brand,
                        Model = Car.Model,
                        RegistrationNumber = Car.RegistrationNumber,
                        Address = Car.Address,
                        Color = Car.Color,
                        ImageUrl = Car.ImageUrl,
                        PricePerDay = Car.PricePerDay,
                        IsRented = Car.IsActive,
                    }
                }
            };

            var result = await carService.AllAsync(queryModel);

            Assert.NotNull(result);
        }

        [Test]
        public async Task AllByAgentIdAsync_ReturnsCorrectCars()
        {
            string agentId = AgentUser.Id.ToString();


            var result = await carService.AllByAgentIdAsync(agentId);

            Assert.NotNull(result);
        }

        [Test]
        public async Task AllByAgentIdAsync_ShouldNotWorkCorrectly()
        {
            string agentId = RenterUser.Id.ToString();


            var result = await carService.AllByAgentIdAsync(agentId);

            Assert.NotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task AllByUserIdAsync_ReturnsCorrectCars()
        {
            string userId = RenterUser.Id.ToString();


            var result = await carService.AllByUserIdAsync(userId);

            Assert.NotNull(result);
        }

        [Test]
        public async Task AllByUserIdAsync_ShouldNotWorkCorrectly()
        {
            string userId = "user123";


            var result = await carService.AllByUserIdAsync(userId);

            Assert.IsEmpty(result);
        }

        [Test]
        public void GetDetailsByIdAsync_ReturnsCorrectDetails()
        {
            var newCar = Car;
            
            var result =  carService.GetDetailsByIdAsync(newCar.Id.ToString());

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCompleted);
        }

        [Test]
        public async Task ExistByIdAsync_ReturnsCorrectCar()
        {
            string carId = Car.Id.ToString();

            var result = carService.ExistByIdAsync(carId);

            Assert.NotNull(result);
        }

        [Test]
        public void ExistByIdAsync_ShouldNotWorkCorrectly()
        {
            string carId = "car123";

            var result = carService.ExistByIdAsync(carId);

            Assert.IsFalse(result.Result);
        }

        [Test]
        public void GetCarForEditByIdAsync_ReturnsCorrectCar()
        {
            string carId = Car.Id.ToString();

            var result = carService.GetCarForEditByIdAsync(carId);

            Assert.NotNull(result);
        }

        [Test]
        public void IsAgentWithIdOwnerOfCarWithIdAsync_WorkCorrect()
        {
            var newCar = Car;
            newCar.AgentId = AgentUser.Id;

            string carId = newCar.Id.ToString();
            string agentId = newCar.AgentId.ToString();

            var result = carService.IsAgentWithIdOwnerOfCarWithIdAsync(carId, agentId);

            Assert.IsTrue(result.IsCompleted);
        }

        [Test]
        public void GetCarForDeleteByIdAsync_WorkCorrect()
        {
            string carId = Car.Id.ToString();

            var result = carService.GetCarForDeleteByIdAsync(carId);

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetCarForDeleteByIdAsync_NotWorkCorrect()
        {
            string carId = "car123";

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await carService.GetCarForDeleteByIdAsync(carId));
        }

        [Test]
        public async Task DeleteByIdAsync_ShouldDeleteWithExistCarId()
        {
            var deletedCar = Car;

            await carService.DeleteByIdAsync(deletedCar.Id.ToString());

            Assert.IsFalse(deletedCar.IsActive);
        }

        [Test]
        public void IsRentedByIdAsync_WorkCorrectly()
        {
            string carId = Car.Id.ToString();

            var result = carService.IsRentedByIdAsync(carId);

            var hasRenter = Car.RenterId.HasValue;
            
            Assert.NotNull(result);
            Assert.That(hasRenter, Is.True);
        }

        [Test]
        public void IsRentedByIdAsync_NotWorkCorrectly()
        {
            string carId = "car123";

            var result = carService.IsRentedByIdAsync(carId);

            Assert.That(result.IsFaulted);
        }

        [Test]
        public async Task RentCarAsync_WorkCorrect()
        {
            var newCar = Car;
            var newUser = RenterUser;

            await carService.RentCarAsync(newCar.Id.ToString(), newUser.Id.ToString());

            bool result = newCar.RenterId.HasValue;

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsRentedByUserWithIdAsync_WorkCorrect()
        {
            var newCar = Car;
            var newUser = RenterUser;

            await carService.RentCarAsync(newCar.Id.ToString(), newUser.Id.ToString());

            var result = carService.IsRentedByUserWithIdAsync(newCar.Id.ToString(), newUser.Id.ToString());

            Assert.IsTrue(result.IsCompleted);
        }

        [Test]
        public async Task IsRentedByUserWithIdAsync_NotWorkCorrect()
        {
            var newCar = Car;
            var newUser = RenterUser;

            newCar.RenterId = Guid.Parse(AgentUser.Id.ToString());

            var result = await carService.IsRentedByUserWithIdAsync(newCar.Id.ToString(), newUser.Id.ToString());

            Assert.IsFalse(result);
        }

        [Test]
        public void LeaveAsync_ShouldLeaveCarWhenIsRented()
        {
            var newCar = Car;

            carService.LeaveAsync(newCar.Id.ToString());

            Assert.IsNotNull(newCar);
            Assert.IsNull(newCar.RenterId);
        }
    }
}
