using Microsoft.AspNetCore.Http;
using OpenIddictDotnet7.Core.AspnetCore.Identity;

namespace OpenIddictDotnet7.Core.AspnetCore.HttpUserService;

public class HttpUserService:IHttpUserService
{
    private readonly IHttpContextAccessor _httpContext;

    public HttpUserService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }
    public UserPrincipal GetCurrentUser()
    {
        if (_httpContext.HttpContext != null && _httpContext.HttpContext.User != null)
        {
            return new UserPrincipal(_httpContext.HttpContext.User);
        }

        throw new Exception("Current user cannot be determined");
    }
}