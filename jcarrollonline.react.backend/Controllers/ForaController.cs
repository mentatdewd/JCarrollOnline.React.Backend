using jcarrollonline.react.backend.Data;
using JCarrollOnlineV2.Entities;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using jcarrollonline.react.backend.Models.ViewModels;

namespace jcarrollonline.react.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForaController : ControllerBase
    {
        private ApplicationDbContext _applicationDbContext;

        public ForaController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("fora-list")]
        public ActionResult ForaList()
        {
            DbSet<Forum> fora = _applicationDbContext.Forum;

            var forumDTOs = fora.Select(f => new ForumDTO
            {
                Title = f.Title,
                Description = f.Description,
                ThreadCount = 0,
                LastThreadPost = "",
            });

            return Ok(forumDTOs.ToList());
    }

}
}
