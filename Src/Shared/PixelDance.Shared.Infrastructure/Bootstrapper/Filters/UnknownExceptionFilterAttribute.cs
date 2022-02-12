using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PixelDance.Shared.Infrastructure.Bootstrapper.Filters
{
    internal class UnknownExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            HandleUnknownException(context);
        }
        private static void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            if (context.Exception != null)
            {
                var errorMessage = context.Exception.Message;
                var innerException = context.Exception.InnerException;
                while (innerException != null)
                {
                    errorMessage += $"\r{innerException.Message}";
                    innerException = innerException.InnerException;
                }
                details.Detail = errorMessage;
            }

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
