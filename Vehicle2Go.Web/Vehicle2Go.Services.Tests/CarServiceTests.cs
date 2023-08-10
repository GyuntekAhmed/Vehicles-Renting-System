using Vehicle2Go.Data.Models.Vehicle;
using Vehicle2Go.Web.ViewModels.Vehicle;

namespace Vehicle2Go.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    
    using Vehicle2Go.Data;
    using Data;
    using Data.Interfaces;

    using static SeederDb;
    using Vehicle2Go.Web.ViewModels.Vehicle.Enums;

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
        public async Task GetDetailsByIdAsync_ReturnsCorrectDetails()
        {
            string carId = Car.Id.ToString();

            var result = await carService.GetDetailsByIdAsync(carId);

            Assert.NotNull(result);
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
        public async Task GetCarForEditByIdAsync_ReturnsCorrectCar()
        {
            string carId = Car.Id.ToString();

            VehicleFormModel model = new VehicleFormModel
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

            var result = await carService.GetCarForEditByIdAsync(carId);

            Assert.IsNotNull(model);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task IsAgentWithIdOwnerOfCarWithIdAsync_WorkCorrect()
        {
            string carId = Car.Id.ToString();
            string agentId = Car.AgentId.ToString();

            var result = await carService.IsAgentWithIdOwnerOfCarWithIdAsync(carId, agentId);
            
            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsAgentWithIdOwnerOfCarWithIdAsync_NotWorkCorrect()
        {
            string carId = Car.Id.ToString();
            string agentId = "aaa";

            var result = await carService.IsAgentWithIdOwnerOfCarWithIdAsync(carId, agentId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task EditCarByIdAndFormModelAsync_WorkCorrect()
        {
            string carId = Car.Id.ToString();

            VehicleFormModel carFormModel = new VehicleFormModel
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

            await carService.EditCarByIdAndFormModelAsync(carId, carFormModel);
            
            Assert.That(Car.Brand, Is.EqualTo(carFormModel.Brand));
            Assert.That(Car.Model, Is.EqualTo(carFormModel.Model));
            Assert.That(Car.RegistrationNumber, Is.EqualTo(carFormModel.RegistrationNumber));
            Assert.That(Car.Address, Is.EqualTo(carFormModel.Address));
            Assert.That(Car.PricePerDay, Is.EqualTo(carFormModel.PricePerDay));
            Assert.That(Car.ImageUrl, Is.EqualTo(carFormModel.ImageUrl));
            Assert.That(Car.Color, Is.EqualTo(carFormModel.Color));
            Assert.That(Car.CategoryId, Is.EqualTo(carFormModel.CategoryId));
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
            var carId = Car.Id;

            var deletedCar = Car;
            deletedCar.Id = carId;

            await carService.DeleteByIdAsync(deletedCar.Id.ToString());

            Assert.IsFalse(deletedCar.IsActive);
        }
    }
}
