using jcarrollonline.react.backend.Data;
using jcarrollonline.react.backend.Models.Entities;
using jcarrollonline.react.backend.Models.ViewModels;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.API.Data.Models;

namespace jcarrollonline.react.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private ApplicationDbContext _applicationDbContext;

        public UsersController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet()]
        public ActionResult Users()
        {
            DbSet<ApplicationUser> users = _applicationDbContext.Users;

            List<UserDTO> userDTOs = users.Select(u => new UserDTO() { Name = u.UserName }).ToList();

            return Ok(userDTOs);
        }
    }
}
