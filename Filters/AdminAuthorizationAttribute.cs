using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace DoAn.Filters
{
    public class AdminAuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userType = context.HttpContext.Session.GetString("UserType");

            if (string.IsNullOrEmpty(userType))
            {
                // Redirect to a non-authorized page or handle accordingly
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "Authentication" });
                return;
            }

            // Check if the user is an admin (adjust based on your actual UserType values)
            if (userType != "admin")
            {
                // Redirect to a non-authorized page or handle accordingly
                context.Result = new RedirectToActionResult("Index", "Home", new { area = "" });
            }

            base.OnActionExecuting(context);
        }
    }
}
