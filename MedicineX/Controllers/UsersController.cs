using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicineX.DBContext;
using MedicineX.DbModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedicineX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _config;

        public UsersController(DatabaseContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return Ok(await _context.Users.ToListAsync());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult> PostUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'DatabaseContext.Users'  is null.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("Register successfully");
        }
        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult> LoginUser(Login user)
        {
            var exuser=_context.Users.FirstOrDefault(u=>u.Email==user.Email && u.Password==user.Password);
            if(exuser == null)
            {
                return NotFound("user Not Found");
            }
            string jwt = JwtTokenCreate(exuser.Email,exuser.UserType,exuser.Id.ToString());

            return Ok(jwt);
        }
        private string JwtTokenCreate(string email, string role, string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                    new Claim("Email",email),
                    new Claim("Role",role),
                    new Claim("UserId",userId),
                    new Claim(ClaimTypes.Role,role),
            };
            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: credential
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
