using AutoMapper;
using GamzeBlogPsikolog.Entity;
using GamzeBlogPsikolog.Entity.Interfaces;
using GamzeBlogPsikolog.EntityViewModels;
using GamzeBlogPsikolog.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamzeBlogPsikolog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialController : Controller
    {
        private readonly IGenericRepostory<Social> srp;
        private readonly IMapper _mapper;

        public SocialController(IGenericRepostory<Social> srp, IMapper mapper)
        {
            this.srp = srp;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> SocialIndex()
        {
            var socialAll = await srp.GetByIdAsync(x => x.SocialId == 2);
            var social = _mapper.Map<SocialViewModel>(socialAll);
            return View(social);
        }

        [HttpPost]
        public IActionResult AddSocial([FromBody] SocialViewModel sm)
        {
            AlertContent alert = new AlertContent();
            try
            {
                if (ModelState.IsValid)
                {
                    if (sm.SocialId == 0)
                    {
                        try
                        {
                            var socialAdd = _mapper.Map<Social>(sm);
                            sm.CreateDate = DateTime.Now;
                            srp.Add(socialAdd);
                            alert.Message = "Kayıt Başarıyla Eklendi";
                            return Json(socialAdd);

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        try
                        {
                            Social socialUpdate = _mapper.Map<Social>(sm);
                            sm.UpdateDate = DateTime.Now;
                            srp.Update(socialUpdate);
                            alert.Message = "Kayıt Başarıyla Eklendi";
                            return Json(socialUpdate);

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Json(alert);

        }
    }
}
