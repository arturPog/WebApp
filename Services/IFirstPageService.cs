using WebApp.ViewModels;

namespace WebApp.Services
{
    public interface IFirstPageService
    {
        Task<PageViewModel> GetFirstPageViewModelAsync();
    }
}
