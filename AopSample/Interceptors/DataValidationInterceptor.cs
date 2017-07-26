using AopSample.Helper;
using AopSample.IoC;
using AopSample.Validation;
using System.Reflection;

namespace AopSample.Interceptors
{
    public class DataValidationInterceptor : IServiceInterceptor
    {
        private readonly ICurrentContext currentContext;

        public DataValidationInterceptor(ICurrentContext currentContext) {
            this.currentContext = currentContext;
        }

        public int AfterOrder => 1;

        public int BeforeOrder => 1;

        public void BeforeProceed(InvocationContext context) {
            foreach (var item in context.Arguments) {
                if (item == null)
                    continue;

                var itemType = item.GetType();
                if (itemType.GetTypeInfo().IsPrimitive || itemType.IsArray)
                    continue;

                var type = typeof(ICustomValidator<>).MakeGenericType(item.GetType());
                var validator = (ICustomValidator)Container.Resolve(type);
                if (validator == null)
                    continue;

                validator.Validate(item, currentContext.Current.ActionType);
            }
        }

        public void AfterProceed(InvocationContext context) {
        }
    }
}