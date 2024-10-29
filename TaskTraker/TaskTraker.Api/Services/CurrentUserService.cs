using System.Security.Claims;
using TaskTraker.Data.Models;
using TaskTraker.Services.Interfaces;

namespace TaskTraker.Api.Services
{
    public class CurrentUserService(IHttpContextAccessor accessor) : ICurrentUserService
    {
        private readonly HttpContext _context = accessor.HttpContext!;

        public string UserId => _context!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}

