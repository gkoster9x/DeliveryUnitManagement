using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryUnitManager.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string? Role { get; set; }
        public  void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!string.IsNullOrEmpty(Role) && (Role=="admin")) context.Result = new ForbidResult();
            //if (context.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    if (!context.HttpContext.User.HasClaim(x => x.Type == "UserType" && x.Value == Role))
            //    {
            //        context.Result = new ForbidResult();
            //    }
            //}
        }

    }
}
