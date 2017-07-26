using AopSample.Interceptors;
using Castle.DynamicProxy;
using System.Linq;

namespace AopSample.IoC
{
    public class ServiceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation) {
            var context = new InvocationContext() {
                Arguments = invocation.Arguments,
                GenericArguments = invocation.GenericArguments,
                Method = invocation.Method,
                InvocationTarget = invocation.InvocationTarget,
                MethodInvocationTarget = invocation.MethodInvocationTarget,
                Proxy = invocation.Proxy,
                ReturnValue = invocation.ReturnValue,
                TargetType = invocation.TargetType
            };

            var interceptors = Container.ResolveAll<IServiceInterceptor>();
            if (interceptors != null) {
                foreach (var interceptorItem in interceptors.OrderBy(d => d.BeforeOrder)) {
                    interceptorItem.BeforeProceed(context);
                }
            }

            invocation.Proceed();

            if (interceptors != null) {
                foreach (var interceptorItem in interceptors.OrderBy(d => d.AfterOrder)) {
                    interceptorItem.AfterProceed(context);
                }
            }
        }
    }
}