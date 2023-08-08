using Vehicle2Go.Web.ViewModels.Rent;

namespace Vehicle2Go.Services.Data.Interfaces
{
    public interface IRentService
    {
        Task<IEnumerable<RentViewModel>> AllRentsAsync();
    }
}
