using Microsoft.AspNetCore.Mvc;

namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Category;
    using Web.ViewModels.Home;

    public interface ICarService
    {
        Task<IEnumerable<VehicleSelectCategoryViewModel>> AllCategoriesAsync();
    }
}
