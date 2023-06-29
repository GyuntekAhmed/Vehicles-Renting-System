namespace VehiclesRenting.Services
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Interfaces;
    using System.Collections.Generic;
    using Web.ViewModels.Car;

    public class CarService : ICarService
    {
        private readonly VehiclesRentingDbContext dbContext;

        public CarService(VehiclesRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllCarsViewModel>> AllCarsAsync()
        {
            return await dbContext
                .Cars
                .Select(c => new AllCarsViewModel()
                {
                    Brand = c.Brand,
                    Model = c.Model,
                    RegistrationNumber = c.RegistrationNumber,
                    CurrentAddress = c.CurrentAddress,
                    PricePerDay = c.PricePerDay,
                    CreatedOn = c.CreatedOn,
                    Color = c.Color,
                    ImageUrl = c.ImageUrl,
                })
                .ToListAsync();
        }
    }
}
