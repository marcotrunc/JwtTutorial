using System.Security.Claims;

namespace JwtTutorial.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService (IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;
        public string GetMyName() => (_httpContextAccessor.HttpContext != null) ? _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name) : String.Empty;
        public string GetHttpContext() => _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
        public string GetEmployee() => (_httpContextAccessor.HttpContext != null) ? _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role) : String.Empty;
    }
}
