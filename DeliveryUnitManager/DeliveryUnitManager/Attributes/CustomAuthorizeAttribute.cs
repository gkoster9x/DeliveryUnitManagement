using DeliveryUnitManager.Reponsitory.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DeliveryUnitManager.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string? Role { get; set; }
        public  void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!string.IsNullOrEmpty(Role) && (Role=="admin")) context.Result = new ForbidResult();
            var user = (Users)context.HttpContext.Items["User"];
            if (user != null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }  
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
