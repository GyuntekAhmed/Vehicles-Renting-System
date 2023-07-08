﻿namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data;
    using Interfaces;
    using Web.ViewModels.Home;

    public class JetService : IJetService
    {
        private readonly VehicleDbContext dbContext;

        public JetService(VehicleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> AllJetsAsync()
        {
            return await dbContext
                .Jets
                .Select(j => new IndexViewModel
                {
                    Id = j.Id.ToString(),
                    Brand = j.Brand,
                    ImageUrl = j.ImageUrl,
                })
                .ToArrayAsync();
        }
    }
}