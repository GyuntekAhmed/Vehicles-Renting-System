namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Home;

    public interface ICarService
    {
        Task<IEnumerable<IndexViewModel>> AllCarsAsync();
    }
}
