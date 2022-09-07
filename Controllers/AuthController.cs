using JwtTutorial.Services.UserService;
using JwtTutorial.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JwtTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;     
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService, DataContext context)
        {
            _context = context;
            _configuration = configuration;
            _userService = userService;
        }
        
        [HttpGet("getusername"), Authorize]
        public ActionResult<object> GetMe() => Ok(_userService.GetMyName());

        [HttpGet("getRefreshToken"), Authorize]
        public ActionResult<string> Tutorial() => _userService.GetHttpContext();

        [HttpGet("getEmployee"), Authorize]
        public ActionResult<string> getEmployee() => _userService.GetEmployee();

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto req)
        {
            CreatePasswordHash(req.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User
            {
                Username = req.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto req)
        {
            var user = await _context.Users.FindAsync(req.Username);
            if (user == null)
                return BadRequest("User Not Found");
            if(!VerifyPasswordHash(req.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Wrong password.");

            //refreshToken
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, user);
            return Ok(CreateToken(user));
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
            };
            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires

            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            //setting user's property
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            _context.SaveChanges();
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            //if we had a DB, here we would have to search inside to see which user owns this cookie
            var user = await _context.Users.Where(u => u.RefreshToken == refreshToken).FirstAsync();
            if(!user.RefreshToken.Equals(refreshToken)|| user == null)
                return Unauthorized("Invalid Refresh Token");
            else if (user.TokenExpires < DateTime.Now)
                return Unauthorized("Token Expired");

            string token = CreateToken(user);
            var newRefreshToken= GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, user);
            return Ok(token);
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role, user.Employee.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(_configuration.GetSection("Jwt:Issuer").Value,
                _configuration.GetSection("Jwt:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
