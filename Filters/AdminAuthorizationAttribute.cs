using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace DoAn.Filters
{
    public class AdminAuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isAdmin = context.HttpContext.Session.GetString("IsAdmin");

            if (string.IsNullOrEmpty(isAdmin) || !bool.Parse(isAdmin))
            {
                // Redirect to a non-authorized page or handle accordingly
                context.Result = new RedirectToActionResult("Index", "Home", new { area = "" });
            }

            base.OnActionExecuting(context);
        }
    }
}
