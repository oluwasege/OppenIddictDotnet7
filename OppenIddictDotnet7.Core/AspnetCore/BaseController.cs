using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using OppenIddictDotnet7.Core.AspnetCore.Identity;
using OppenIddictDotnet7.Core.Enums;
using OppenIddictDotnet7.Core.Extensions;
using OppenIddictDotnet7.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
            _logger.Error(ex.StackTrace, ex);
            var response = new ApiResponse<string>();

#if DEBUG
            response.Errors.Add($"Error: {ex?.InnerException?.Message ?? ex?.Message} --> {ex?.StackTrace}");
            return Ok(response);
#else
            rsp.Errors.Add(customErrorMessage ?? "An error occurred while processing your request!");
            return Ok(rsp);
#endif
        }
        public IActionResult ApiResponse<T>(T data = default,
                                            string message = "",
                                            ApiResponseCodes codes = ApiResponseCodes.OK,
                                            int? totalCount = 0,
                                            params string[] errors)
        {
            var response = new ApiResponse<T>(data, message, codes, totalCount, errors);
            response.Description = message ?? response.Code.GetDescription();
            return Ok(response);
        }
    }
}
