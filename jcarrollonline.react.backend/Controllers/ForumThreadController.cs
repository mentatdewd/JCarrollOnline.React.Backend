using jcarrollonline.react.backend.Data;
using jcarrollonline.react.backend.Models.Entities;
using jcarrollonline.react.backend.Models.ViewModels;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jcarrollonline.react.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumThreadController : ControllerBase
    {
        private ApplicationDbContext _applicationDbContext;

        public ForumThreadController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("{threadId}")]
        public ActionResult ForumThread(int parentThreadId)
        {
            DbSet<ForumThread> forumThreads = _applicationDbContext.ForumThread;

            IQueryable<ForumThreadDTO> forumThreadDTOs = forumThreads.Where(f => f.ParentId == parentThreadId).Select(f => new ForumThreadDTO
            {
                Id = f.Id,
                Title = f.Title,
                Description = f.Content,
                PostCount = 0,
                LastPost = "",
                LastPostedDate = DateTime.Now,
            });

            return Ok(forumThreadDTOs.ToList());
        }
    }
}
