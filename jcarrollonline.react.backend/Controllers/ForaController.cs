using jcarrollonline.react.backend.Data;
using jcarrollonline.react.backend.Models;
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
        public async Task<ActionResult> Fora()
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

            return Ok(await forumDTOs.ToListAsync());
        }

        [HttpGet("{forumId}")]
        public async Task<ActionResult> ForumThreadList(int forumId)
        {
            DbSet<ForumThread> forumThreads = _applicationDbContext.ForumThread;

            Forum? forum = await _applicationDbContext.Forum.FirstOrDefaultAsync(f => f.Id == forumId);

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
                    Content = f.Content,
                    PostCount = 0,
                    LastPost = "",
                    LastPostedDate = DateTime.Now,
                })],
            };

            return Ok(forumThreadDTOs);
        }

        [HttpGet("{forumId}/{threadId}")]
        public async Task<ActionResult> ForumThread(int forumId, int threadId)
        {
            DbSet<ForumThread> forumThreads = _applicationDbContext.ForumThread;

            IQueryable<ForumThreadDTO> forumThreadDTOs = forumThreads.Where(f => f.ForumId == forumId && f.ParentId == threadId).Select(f => new ForumThreadDTO
            {
                Id = f.Id,
                Title = f.Title,
                Content = f.Content,
                PostCount = 0,
                LastPost = "",
                LastPostedDate = DateTime.Now,
            });

            return Ok(await forumThreadDTOs.ToListAsync());
        }

        [HttpPost("create-forum")]
        public async Task<ActionResult> CreateForum([FromForm] ForumCreateDTO forumDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Check for duplicate
            var exists = await _applicationDbContext.Forum.SingleOrDefaultAsync(f => f.Title == forumDTO.Title);
            if(exists != null)
            {
                return BadRequest($"Forum {forumDTO.Title} already exists"); 
            }

            Forum forum = new Forum()
            {
                Title = forumDTO.Title,
                Description = forumDTO.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            await _applicationDbContext.Forum.AddAsync(forum);
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
