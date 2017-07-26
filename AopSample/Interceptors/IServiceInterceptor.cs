namespace AopSample.Interceptors
{
    public interface IServiceInterceptor
    {
        int BeforeOrder { get; }

        void BeforeProceed(InvocationContext context);

        int AfterOrder { get; }

        void AfterProceed(InvocationContext context);
    }
}