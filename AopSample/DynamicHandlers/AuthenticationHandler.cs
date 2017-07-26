using AopSample.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace AopSample.DynamicHandlers
{
    public class AuthenticationHandler : IDynamicHandler
    {
        private readonly ICurrentContext currentContext;

        public AuthenticationHandler(ICurrentContext currentContext) {
            this.currentContext = currentContext;
        }

        public short Order => 0;

        public void AfterSend(IResponseContext responseContext) {
        }

        public void BeforeSend(IRequestContext requestContext) {
            var context = requestContext.Request as ActionExecutingContext;
            SetContextActionType(context.HttpContext.Request);
            
            var isAllowAnonymous = context.Filters.Any(f => f.GetType() == typeof(AllowAnonymousFilter));

            var auth = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if(!string.IsNullOrEmpty(auth))
            {
                var parameters = auth.Split(' ');
                if (parameters.Length != 2 && !isAllowAnonymous)
                    throw new Exception("InvalidCredential");

                if (parameters[0] != "Basic" && !isAllowAnonymous)
                    throw new Exception("InvalidCredential");

                currentContext.UserName = parameters[1];

            } else if (!isAllowAnonymous)
            {
                throw new Exception("MissingCredential");
            }
        }

        public void OnException(IExceptionContext exceptionContext) {
        }

        private void SetContextActionType(HttpRequest request) {
            if (request.Method == "DELETE") {
                currentContext.ActionType = ActionType.Delete;
            } else if (request.Method == "PUT") {
                currentContext.ActionType = ActionType.Update;
            } else if (request.Method == "POST") {
                currentContext.ActionType = ActionType.Add;
            } else {
                currentContext.ActionType = ActionType.List;
            }
        }
    }
}