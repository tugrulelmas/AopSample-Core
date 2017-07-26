using AopSample.DynamicHandlers;
using AopSample.IoC;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace AopSample.Helper
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (context.Exception == null)
            {
                foreach (var dynamicHandlerItem in dynamicHandlers)
                {
                    dynamicHandlerItem.AfterSend(null);
                }
            }
            else
            {
                IExceptionContext exceptionContext = new DynamicHandlers.ExceptionContext(context);
                foreach (var dynamicHandlerItem in dynamicHandlers)
                {
                    dynamicHandlerItem.OnException(exceptionContext);
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            IRequestContext requestContext = new RequestContext(context);

            foreach (var dynamicHandlerItem in dynamicHandlers)
            {
                dynamicHandlerItem.BeforeSend(requestContext);
            }

            base.OnActionExecuting(context);
        }

        private IEnumerable<IDynamicHandler> dynamicHandlers => Container.ResolveAll<IDynamicHandler>().OrderBy(d => d.Order);
    }
}