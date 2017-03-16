using AiursoftBase.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AiursoftBase.Attributes
{
    /// <summary>
    /// Request the signed in token or throw a NotAiurSignedInException
    /// </summary>
    public class AiurForceAuth : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var controller = context.Controller as Controller;
            if (!controller.User.Identity.IsAuthenticated)
            {
                var queryReturnUrl = controller.HttpContext.Request.Query["returnurl"];

                string stateUrl = string.Empty;

                if (!string.IsNullOrEmpty(queryReturnUrl))
                    stateUrl = queryReturnUrl;
                else
                    stateUrl = $"{controller.Request.Path}";
                throw new NotAiurSignedInException(controller, stateUrl);
            }
        }
    }
}
