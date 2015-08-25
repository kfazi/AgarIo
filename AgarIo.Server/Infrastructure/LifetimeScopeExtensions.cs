namespace AgarIo.Server.Infrastructure
{
    using Autofac;

    public static class LifetimeScopeExtensions
    {
        public static ILifetimeScope BeginServiceLifetimeScope(this ILifetimeScope lifetimeScope)
        {
            return lifetimeScope.BeginLifetimeScope(LifetimeScopeTags.Service);
        }
    }
}