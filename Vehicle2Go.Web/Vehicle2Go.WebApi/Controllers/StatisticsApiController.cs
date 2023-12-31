﻿namespace Vehicle2Go.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Models.Statistics;
    using Services.Data.Interfaces;

    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly ICarService carService;
        private readonly IMotorcycleService motorcycleService;
        private readonly IJetService jetService;
        private readonly IYachtService yachtService;
        private readonly ITruckService truckService;

        public StatisticsApiController(ICarService carService,
                                       IMotorcycleService motorcycleService,
                                       IJetService jetService,
                                       IYachtService yachtService,
                                       ITruckService truckService)
        {
            this.carService = carService;
            this.motorcycleService = motorcycleService;
            this.jetService = jetService;
            this.yachtService = yachtService;
            this.truckService = truckService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                var carServiceModel = await this.carService.GetStatisticsAsync();
                var motorcycleServiceModel = await this.motorcycleService.GetStatisticsAsync();
                var jetServiceModel = await this.jetService.GetStatisticsAsync();
                var yachtServiceModel = await this.yachtService.GetStatisticsAsync();
                var truckServiceModel = await this.truckService.GetStatisticsAsync();

                carServiceModel.TotalVehicles +=
                    motorcycleServiceModel.TotalVehicles +=
                        jetServiceModel.TotalVehicles +=
                            yachtServiceModel.TotalVehicles +=
                                truckServiceModel.TotalVehicles;

                carServiceModel.TotalRents +=
                    motorcycleServiceModel.TotalRents +=
                        jetServiceModel.TotalRents +=
                            truckServiceModel.TotalRents +=
                                yachtServiceModel.TotalRents;

                return this.Ok(carServiceModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
