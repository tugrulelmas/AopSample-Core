using AopSample.ApplicationServices;
using AopSample.DynamicHandlers;
using AopSample.Helper;
using AopSample.Interceptors;
using AopSample.IoC;
using AopSample.Validation;
using Castle.Core;

namespace AopSample
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Container.Register<ServiceInterceptor>();
            Container.Register<ICurrentContext, CurrentContext>(LifestyleType.Scoped);
            Container.RegisterWithBase(typeof(ICustomValidator<>), typeof(CustomValidator<>));
            Container.RegisterServices<IService, UserService>();

            Container.Register<IServiceInterceptor, DataValidationInterceptor>();

            Container.Register<IDynamicHandler, AuthenticationHandler>(LifestyleType.Scoped);
        }
    }
}