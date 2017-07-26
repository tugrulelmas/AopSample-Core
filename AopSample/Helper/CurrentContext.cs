using AopSample.IoC;

namespace AopSample.Helper
{
    public class CurrentContext : ICurrentContext
    {
        public string UserName { get; set; }

        public ActionType ActionType { get; set; }

        public ICurrentContext Current => Container.Resolve<ICurrentContext>();
    }
}