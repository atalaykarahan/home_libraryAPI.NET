using AutoMapper;
using home_libraryAPI.DTOs;
using home_libraryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace home_libraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HomeLibraryContext _context;
        private readonly IMapper _mapper;

        public UsersController(HomeLibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto userObj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userObj.UserName);

            if (user == null)
            {
                return NotFound(new { Message = "Kullanıcı bulunamadı" });
            }

            if (userObj.Password != user.Password)
            {
                return BadRequest(new { Message = "Şifre yanlış" });
            }

            var userToken = CreateJwt(user);

            return Ok(new { Token = userToken, Message = "Giriş başarılı" });
        }

        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("wubbalubbadubdub");
            var identity = new ClaimsIdentity(new Claim[]
            {
               new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
               new Claim(ClaimTypes.Role, Convert.ToString(user.AuthorityId)),
               new Claim(ClaimTypes.Name, user.UserName)
            });

            var creadentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creadentials,
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

    }
}
