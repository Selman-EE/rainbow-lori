using Application.Common;
using Application.Model.Response;
using Application.Service.LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Api.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                ILoggerManager _logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerManager>();
                var message = $"{nameof(UnauthorizedResult)}: Header name must be include key name";
                _logger.LogError(message);
                //
                var errorResponse = new ErrorResponse(new ErrorModel
                {
                    FieldName = nameof(AuthorizationFailure),
                    Message = message
                });
                context.Result = new UnauthorizedObjectResult(errorResponse);
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("AllKeys:ApiKey");
            if (!apiKey.Equals(potentialApiKey))
            {
                ILoggerManager _logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerManager>();
                var message = $"{nameof(UnauthorizedResult)}: Api key is wrong";
                _logger.LogError(message);
                //
                var errorResponse = new ErrorResponse(new ErrorModel
                {
                    FieldName = nameof(AuthorizationFailure),
                    Message = message
                });

                context.Result = new UnauthorizedObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}
