using System.Diagnostics.CodeAnalysis;
using log4net;
using Microsoft.AspNetCore.Mvc;
using OpenIddictDotnet7.Core.AspnetCore.Identity;
using OpenIddictDotnet7.Core.Enums;
using OpenIddictDotnet7.Core.Extensions;
using OpenIddictDotnet7.Core.ViewModels;

namespace OpenIddictDotnet7.Core.AspnetCore
{
    public class BaseController : ControllerBase
    {
        private readonly ILog _logger;
        public BaseController()
        {
            _logger = LogManager.GetLogger(typeof(BaseController));
        }

        protected UserPrincipal CurrentUser => new UserPrincipal(User);

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
        public IActionResult ApiResponse<T>([DisallowNull] T data = default!,
                                            string message = "",
                                            ApiResponseCodes codes = ApiResponseCodes.OK,
                                            int? totalCount = 0,
                                            params string[] errors)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            var response = new ApiResponse<T>(data, message, codes, totalCount, errors);
            response.Description = message ?? response.Code.GetDescription();
            return Ok(response);
        }
    }
}
