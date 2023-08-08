namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using Vehicle2Go.Data;
    using Web.ViewModels.Rent;

    public class RentService : IRentService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public RentService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<RentViewModel>> AllRentsAsync()
        {
            List<RentViewModel> allRents = new List<RentViewModel>();

            IEnumerable<RentViewModel> allCarRents = await dbContext
                .Cars
                .Include(c => c.Renter)
                .Include(c => c.Agent)
                .ThenInclude(a => a.User)
                .Where(c => c.RenterId.HasValue)
                .Select(c => new RentViewModel
                {
                    Brand = c.Brand,
                    ImageUrl = c.ImageUrl,
                    AgentFullName = c.Agent.User.FirstName + " " + c.Agent.User.LastName,
                    AgentEmail = c.Agent.User.Email,
                    RenterEmail = c.Renter.Email,
                    RenterFullName = c.Renter.FirstName + " " + c.Renter.LastName
                })
                .ToArrayAsync();

            IEnumerable<RentViewModel> allMotorcyclesRents = await dbContext
                .Motorcycles
                .Include(m => m.Renter)
                .Include(m => m.Agent)
                .ThenInclude(a => a.User)
                .Where(m => m.RenterId.HasValue)
                .Select(m => new RentViewModel
                {
                    Brand = m.Brand,
                    ImageUrl = m.ImageUrl,
                    AgentFullName = m.Agent.User.FirstName + " " + m.Agent.User.LastName,
                    AgentEmail = m.Agent.User.Email,
                    RenterEmail = m.Renter.Email,
                    RenterFullName = m.Renter.FirstName + " " + m.Renter.LastName
                })
                .ToArrayAsync();

            IEnumerable<RentViewModel> allJetsRents = await dbContext
                .Jets
                .Include(j => j.Renter)
                .Include(j => j.Agent)
                .ThenInclude(a => a.User)
                .Where(j => j.RenterId.HasValue)
                .Select(j => new RentViewModel
                {
                    Brand = j.Brand,
                    ImageUrl = j.ImageUrl,
                    AgentFullName = j.Agent.User.FirstName + " " + j.Agent.User.LastName,
                    AgentEmail = j.Agent.User.Email,
                    RenterEmail = j.Renter.Email,
                    RenterFullName = j.Renter.FirstName + " " + j.Renter.LastName
                })
                .ToArrayAsync();

            IEnumerable<RentViewModel> allYachtsRents = await dbContext
                .Yachts
                .Include(y => y.Renter)
                .Include(y => y.Agent)
                .ThenInclude(a => a.User)
                .Where(y => y.RenterId.HasValue)
                .Select(y => new RentViewModel
                {
                    Brand = y.Brand,
                    ImageUrl = y.ImageUrl,
                    AgentFullName = y.Agent.User.FirstName + " " + y.Agent.User.LastName,
                    AgentEmail = y.Agent.User.Email,
                    RenterEmail = y.Renter.Email,
                    RenterFullName = y.Renter.FirstName + " " + y.Renter.LastName
                })
                .ToArrayAsync();

            IEnumerable<RentViewModel> allTrucksRents = await dbContext
                .Trucks
                .Include(t => t.Renter)
                .Include(t => t.Agent)
                .ThenInclude(a => a.User)
                .Where(t => t.RenterId.HasValue)
                .Select(t => new RentViewModel
                {
                    Brand = t.Brand,
                    ImageUrl = t.ImageUrl,
                    AgentFullName = t.Agent.User.FirstName + " " + t.Agent.User.LastName,
                    AgentEmail = t.Agent.User.Email,
                    RenterEmail = t.Renter.Email,
                    RenterFullName = t.Renter.FirstName + " " + t.Renter.LastName
                })
                .ToArrayAsync();

            allRents.AddRange(allTrucksRents);
            allRents.AddRange(allYachtsRents);
            allRents.AddRange(allJetsRents);
            allRents.AddRange(allCarRents);
            allRents.AddRange(allMotorcyclesRents);

            return allRents;
        }
    }
}
