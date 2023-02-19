using OppenIddictDotnet7.Core.AspnetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OppenIddictDotnet7.Core.AspnetCore.HttpUserService
{
    public interface IHttpUserService
    {
        UserPrincipal GetCurrentUser();
    }
}
