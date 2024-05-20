using jcarrollonline.react.backend.Data;
using jcarrollonline.react.backend.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jcarrollonline.react.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadController : ControllerBase
    {
        private ApplicationDbContext _applicationDbContext;

        public ThreadController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("{rootPostId}")]
        public async Task<ActionResult> Posts(int rootPostId)
        {
            DbSet<ForumThread> posts = _applicationDbContext.ForumThread;

            var forumThreadDTOs = posts.Include("Author").Where(f => f.Id == rootPostId || f.RootId == rootPostId);
            
            //.Select(f => new ForumThreadDTO
            //{
            //    Id = f.Id,
            //    Title = f.Title,
            //    Content = f.Content,
            //    PostCount = 0,
            //    LastPost = "",
            //    LastPostedDate = DateTime.Now,
            //    Author = f.Author.UserName,
            //});

            //foreach(var forumThreadDTO in forumThreadDTOs)
            //{
            //    DbSet<ApplicationUser> user = _applicationDbContext.Users;

            //}
            return Ok(await forumThreadDTOs.ToListAsync());
        }
    }
}
