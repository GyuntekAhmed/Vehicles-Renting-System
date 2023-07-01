namespace VehiclesRenting.Services.Interfaces
{
    using System.Threading.Tasks;

    using Web.ViewModels.Category;

    public interface ICategoryService
    {
        Task<IEnumerable<VehicleCategoryViewModel>> AllCategoriesAsync();
    }
}
