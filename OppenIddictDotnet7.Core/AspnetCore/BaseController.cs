using log4net;
using Microsoft.AspNetCore.Mvc;
using OppenIddictDotnet7.Core.AspnetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OppenIddictDotnet7.Core.AspnetCore
{
    public class BaseController : ControllerBase
    {
        private readonly ILog _logger;
        public BaseController()
        {
            _logger = LogManager.GetLogger(typeof(BaseController));
        }

        protected UserPrincipal CurrentUser
        {
            get
            {
                return new UserPrincipal(User);
            }
        }

        /// <summary>
        /// To Read Model Errors into a collection of string
        /// </summary>
        protected List<string> ListModelErrors
        {
            get
            {
                return ModelState.Values
                    .SelectMany(x => x.Errors
                    .Select(m => m.ErrorMessage))
                    .ToList();
            }
        }

        protected IActionResult HandleError(Exception ex, string? customErrorMessage = null)
        {

        }
    }
}
