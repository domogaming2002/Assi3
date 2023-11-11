using AutoMapper.Execution;
using BusinessObject.Models;
using DTO.DTO.Request.Member;
using DTO.DTO.Response.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Member = BusinessObject.Models.Member;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly UserManager<Member> _memberManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenController(UserManager<Member> userManager, RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration)
        {
            _memberManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(MemberLogin request)
        {
            var user = await _memberManager.FindByEmailAsync(request.Email);
            if (user != null && await _memberManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _memberManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}" ),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new MemberLoginResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(MemberRegister request)
        {
            var userExists = await _memberManager.FindByEmailAsync(request.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Email da ton tai");

            Member user = new()
            {
                UserName = request.Email,
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            var result = await _memberManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);

            if (!await _roleManager.RoleExistsAsync("Customer"))
                await _roleManager.CreateAsync(new IdentityRole<int>("Customer"));

            if (await _roleManager.RoleExistsAsync("Customer"))
            {
                await _memberManager.AddToRoleAsync(user, "Customer");
            }

            return Ok("Register successfully");
        }

        [HttpPost]
        [Route("Register/Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] MemberRegister request)
        {
            var userExists = await _memberManager.FindByEmailAsync(request.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Email da ton tai");

            Member user = new()
            {
                UserName = request.Email,
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            var result = await _memberManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole<int>("Admin"));
            if (!await _roleManager.RoleExistsAsync("Customer"))
                await _roleManager.CreateAsync(new IdentityRole<int>("Customer"));

            if (await _roleManager.RoleExistsAsync("Admin"))
            {
                await _memberManager.AddToRoleAsync(user, "Admin");
            }

            return Ok("Register successfully");
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
