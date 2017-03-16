using AiursoftBase.Exceptions;
using AiursoftBase.Models;
using AiursoftBase.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Attributes
{
    /// <summary>
    /// This will stop current action with any Aiursoft exceptions.
    /// </summary>
    public class AiurExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            switch (context.Exception.GetType().Name)
            {
                case nameof(NotAiurSignedInException):
                    {
                        var exp = context.Exception as NotAiurSignedInException;
                        var r = context.HttpContext.Request;
                        string ServerPosition = $"{r.Scheme}://{r.Host}";

                        string url = UrlConverter.UrlWithAuth(ServerPosition, exp.SignInRedirectPath);
                        context.ExceptionHandled = true;
                        context.HttpContext.Response.Redirect(url.ToString());
                    }
                    break;
                case nameof(AiurUnexceptedResponse):
                    {
                        var exp = context.Exception as AiurUnexceptedResponse;
                        var arg = new AiurProtocal
                        {
                            code = exp.Response.code,
                            message = exp.Response.message
                        };
                        var url = new AiurUrl(string.Empty, "api", "exception", arg);
                        context.ExceptionHandled = true;
                        context.HttpContext.Response.Redirect(url.ToString());
                    }
                    break;
                case nameof(ModelStateNotValidException):
                    {
                        var exp = context.Exception as ModelStateNotValidException;
                        var arg = new AiurProtocal
                        {
                            code = ErrorType.InvalidInput,
                            message = "Input not valid!"
                        };
                        var url = new AiurUrl(string.Empty, "api", "exception", arg);
                        context.ExceptionHandled = true;
                        context.HttpContext.Response.Redirect(url.ToString());
                    }
                    break;
                default:
                    {
                        var exp = context.Exception as Exception;
                        var arg = new AiurProtocal
                        {
                            code = ErrorType.UnknownError,
                            message = exp.Message
                        };
                        var url = new AiurUrl(string.Empty, "api", "exception", arg);
                        context.ExceptionHandled = true;
                        context.HttpContext.Response.Redirect(url.ToString());
                    }
                    break;
            }
        }
    }
}
