namespace VehiclesRenting.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VehiclesRenting.Web.ViewModels.Home;

    public interface IScooterService
    {
        Task<IEnumerable<IndexViewModel>> AllScootersAsync();
    }
}
