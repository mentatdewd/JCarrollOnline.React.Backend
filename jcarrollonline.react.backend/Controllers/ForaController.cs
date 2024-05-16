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
    public class ForaController : ControllerBase
    {
        private ApplicationDbContext _applicationDbContext;

        public ForaController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet()]
        public ActionResult Fora()
        {
            DbSet<Forum> fora = _applicationDbContext.Forum;

            IQueryable<ForumDTO> forumDTOs = fora.Select(f => new ForumDTO
            {
                Id = f.Id,
                Title = f.Title,
                Description = f.Description,
                ThreadCount = 0,
                LastThreadPost = "",
            });

            return Ok(forumDTOs.ToList());
        }

        [HttpGet("{forumId}")]
        public ActionResult ForumThreadList(int forumId)
        {
            DbSet<ForumThread> forumThreads = _applicationDbContext.ForumThread;

            Forum? forum = _applicationDbContext.Forum.FirstOrDefault(f => f.Id == forumId);

            if (forum == null)
            {
                return BadRequest("Forum not found");
            }

            ForumThreadDTOs forumThreadDTOs = new ForumThreadDTOs()
            {
                ForumTitle = forum.Title,
                Forums = [.. forumThreads.Where(f => f.ForumId == forumId).Select(f => new ForumThreadDTO()
                {
                    Id = f.Id,
                    Title = f.Title,
                    Description = f.Content,
                    PostCount = 0,
                    LastPost = "",
                    LastPostedDate = DateTime.Now,
                })],
            };

            return Ok(forumThreadDTOs);
        }

        [HttpGet("{forumId}/{threadId}")]
        public ActionResult ForumThread(int forumId, int threadId)
        {
            DbSet<ForumThread> forumThreads = _applicationDbContext.ForumThread;

            IQueryable<ForumThreadDTO> forumThreadDTOs = forumThreads.Where(f => f.ForumId == forumId && f.ParentId == threadId).Select(f => new ForumThreadDTO
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
