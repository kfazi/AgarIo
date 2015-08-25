namespace AgarIo.Server.Infrastructure
{
    using System;

    using Autofac;

    using Topshelf;
    using Topshelf.HostConfigurators;
    using Topshelf.ServiceConfigurators;

    public static class HostConfiguratorExtensions
    {
        public static HostConfigurator AutofacService<TService>(this HostConfigurator hostConfigurator, ILifetimeScope lifetimeScope, Action<ServiceConfigurator<TService>> callback)
            where TService : class, IAutofacService
        {
            hostConfigurator.Service<AutofacServiceWrapper<TService>>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(hostSettings => new AutofacServiceWrapper<TService>(lifetimeScope));
                    serviceConfigurator.WhenStarted(s => s.Start());
                    serviceConfigurator.WhenStopped(s => s.Stop());
                });
            return hostConfigurator;
        }
    }
}