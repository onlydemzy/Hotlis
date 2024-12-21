using System.Security.Claims;
using Hotlis.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Hotlis.Infrastructure.Services;

public class UserContextService(IHttpContextAccessor _httpContextAccessor) : IUserContextService
{
    private readonly IHttpContextAccessor httpContextAccessor=_httpContextAccessor;
    public string UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)??string.Empty;
    public string UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name??string.Empty;

}