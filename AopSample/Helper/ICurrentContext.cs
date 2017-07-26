namespace AopSample.Helper
{
    public interface ICurrentContext
    {
        /// <summary>
        /// Gets or sets the type of the action.
        /// </summary>
        /// <value>
        /// The type of the action.
        /// </value>
        ActionType ActionType { get; set; }

        ICurrentContext Current { get; }

        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        /// <value>
        /// The ip.
        /// </value>
        string UserName { get; set; }
    }
}