using AutoMapper;
using GamzeBlogPsikolog.Context;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using Microsoft.EntityFrameworkCore;

namespace GamzeBlogPsikolog.Services
{
    public class EducationService : IEducationService
    {
        private readonly IGenericRepostory<Education> _educationrepo;
        private readonly IMapper _mapper;

        public EducationService(IGenericRepostory<Education> postory, IMapper mapper)
        {
            _educationrepo = postory;
            _mapper = mapper;
        }
        public async Task<List<EducationViewModel>> GetAlltrue()
        {
            var educations = await _educationrepo.GetAll(x => x.Statu == true);
            var mapeducations = _mapper.Map<List<EducationViewModel>>(educations);
            return mapeducations;
        }
        public async Task<List<EducationViewModel>> GetAll()
        {
            var educations = await _educationrepo.GetAll();
            var mapeducations = _mapper.Map<List<EducationViewModel>>(educations);
            return mapeducations;
        }
        public async Task<EducationViewModel> GetByIdAsync(int id)
        {
            var educations = await _educationrepo.GetByIdAsync(x=>x.EducationId == id);
            var mapeducations = _mapper.Map<EducationViewModel>(educations);
            return mapeducations;
        }
        public async Task<string> AddEducation(EducationViewModel e)
        {
            try
            {
                if (e.EducationId == 0)
                {
                    e.Statu = true;
                    Education newEducation = _mapper.Map<Education>(e);
                    await _educationrepo.Add(newEducation);
                    return "Ok";
                }
                else
                {
                    await EditEducation(e);
                    return "Ok";

                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task EditEducation(EducationViewModel e)
        {
            var com = await _educationrepo.GetByIdAsync(x => x.EducationId == e.EducationId);
            com.Name = e.Name;
            com.Statu = true;
            _educationrepo.Update(com);
        }

        public async Task DeleteEducation(int id)
        {
            var com = await _educationrepo.GetByIdAsync(x => x.EducationId == id);
            if (com.Statu)
            {
                com.Statu = false;
            }
            else
            {
                com.Statu = true;
            }
            _educationrepo.Update(com);
        }
    }
}
