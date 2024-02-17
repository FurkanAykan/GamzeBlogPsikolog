using GamzeBlogPsikolog.EntityViewModels;

namespace GamzeBlogPsikolog.Entity.Interfaces
{
    public interface IEducationService
    {
        Task<List<EducationViewModel>> GetAll();
        Task<List<EducationViewModel>> GetAlltrue();
        Task<EducationViewModel> GetByIdAsync(int id);
        Task<string> AddEducation(EducationViewModel e);
        Task EditEducation(EducationViewModel e);
        Task DeleteEducation(int id);
    }
}
