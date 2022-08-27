using System.Security.Claims;

namespace JwtTutorial.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService (IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;
        public string GetMyName() => (_httpContextAccessor.HttpContext != null) ? _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name) : String.Empty;  
        
    }
}
