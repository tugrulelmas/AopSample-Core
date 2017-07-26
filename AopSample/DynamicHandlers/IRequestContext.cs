namespace AopSample.DynamicHandlers
{
    public interface IRequestContext
    {
        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        object Request { get; }
    }
}