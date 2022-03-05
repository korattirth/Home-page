using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public class UserServicesHelper : IUserServicesHelper
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserServicesHelper(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public string getUserId()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
