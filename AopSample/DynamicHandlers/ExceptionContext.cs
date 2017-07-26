namespace AopSample.DynamicHandlers
{
    public class ExceptionContext : IExceptionContext
    {
        public ExceptionContext(object context) {
            Context = context;
        }

        public object Context { get; }
    }
}