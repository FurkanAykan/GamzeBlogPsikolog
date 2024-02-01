using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Entity.Interfaces
{
    public interface IAboutService
    {
        Task<AboutPageViewModel> GetAboutAsync();
    }
}
