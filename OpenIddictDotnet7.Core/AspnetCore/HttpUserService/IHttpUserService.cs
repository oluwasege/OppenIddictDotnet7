using OpenIddictDotnet7.Core.AspnetCore.Identity;

namespace OpenIddictDotnet7.Core.AspnetCore.HttpUserService
{
    public interface IHttpUserService
    {
        UserPrincipal GetCurrentUser();
    }
}
