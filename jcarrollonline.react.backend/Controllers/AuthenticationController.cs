using jcarrollonline.react.backend.Data;
using jcarrollonline.react.backend.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolApp.API.Data.Helpers;
using SchoolApp.API.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jcarrollonline.react.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

        public AuthenticationController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IConfiguration configuration,
            TokenValidationParameters tokenValidationParameters)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all the required fields");
            }

            ApplicationUser? userExists = await _userManager.FindByEmailAsync(registerDTO.EmailAddress);
            if (userExists != null)
            {
                return BadRequest($"User {registerDTO.EmailAddress} already exists");
            }

            ApplicationUser newUser = new ApplicationUser()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.EmailAddress,
                UserName = registerDTO.UserName,
                Custom = "",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, registerDTO.Password);

            IdentityResult result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (result.Succeeded)
            {
                //Add user role

                //switch (registerDTO.Role)
                //{
                //    case UserRoles.Manager:
                //        await _userManager.AddToRoleAsync(newUser, UserRoles.Manager);
                //        break;
                //    case UserRoles.Student:
                //        await _userManager.AddToRoleAsync(newUser, UserRoles.Student);
                //        break;
                //    default:
                //        break;
                //}


                return Ok("User created");
            }
            return BadRequest("User could not be created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required fields");
            }

            ApplicationUser? userExists = await _userManager.FindByEmailAsync(loginDTO.EmailAddress);
            if (userExists != null && await _userManager.CheckPasswordAsync(userExists, loginDTO.Password))
            {
                AuthResultDTO tokenValue = await GenerateJWTTokenAsync(userExists, null);
                return Ok(tokenValue);
            }
            return Unauthorized();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDTO tokenRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required fields");
            }

            AuthResultDTO result = await VerifyAndGenerateTokenAsync(tokenRequestDTO);
            return Ok(result);
        }

        private async Task<AuthResultDTO> VerifyAndGenerateTokenAsync(TokenRequestDTO tokenRequestDTO)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            RefreshToken? storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequestDTO.RefreshToken);
            ApplicationUser? dbUser = await _userManager.FindByIdAsync(storedToken.UserId);

            try
            {
                ClaimsPrincipal tokenCheckResult = jwtTokenHandler.ValidateToken(tokenRequestDTO.Token, _tokenValidationParameters, out SecurityToken? validatedToken);

                return await GenerateJWTTokenAsync(dbUser, storedToken);
            }
            catch (SecurityTokenExpiredException)
            {
                if (storedToken.DateExpire >= DateTime.UtcNow)
                {
                    return await GenerateJWTTokenAsync(dbUser, storedToken);
                }
                else
                {
                    return await GenerateJWTTokenAsync(dbUser, null);
                }
            }
        }

        private async Task<AuthResultDTO> GenerateJWTTokenAsync(ApplicationUser user, RefreshToken rToken)
        {
            List<Claim> authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Add User Role Claims
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            foreach (string userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }


            SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            if (rToken != null)
            {
                AuthResultDTO rTokenResponse = new AuthResultDTO()
                {
                    Token = jwtToken,
                    RefreshToken = rToken.Token,
                    ExpiresAt = token.ValidTo
                };
                return rTokenResponse;
            }

            RefreshToken refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
            };
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();


            AuthResultDTO response = new AuthResultDTO()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = token.ValidTo
            };

            return response;

        }
    }
}