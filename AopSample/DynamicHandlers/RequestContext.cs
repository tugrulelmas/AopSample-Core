namespace AopSample.DynamicHandlers
{
    public class RequestContext : IRequestContext
    {
        public RequestContext(object request) {
            Request = request;
        }

        public object Request { get; }
    }
}