namespace AgarIo.Server.Infrastructure
{
    using System;

    using Autofac;

    internal sealed class AutofacServiceWrapper<T>
        where T : class, IAutofacService
    {
        private readonly ILifetimeScope _applicationLifetimeScope;

        private ILifetimeScope _serviceLifetimeScope;

        private T _service;

        public AutofacServiceWrapper(ILifetimeScope applicationLifetimeScope)
        {
            if (applicationLifetimeScope == null) throw new ArgumentNullException(nameof(applicationLifetimeScope));

            _applicationLifetimeScope = applicationLifetimeScope;
        }

        public void Start()
        {
            _serviceLifetimeScope = _applicationLifetimeScope.BeginServiceLifetimeScope();
            _service = _serviceLifetimeScope.Resolve<T>();
            _service.Start();
        }

        public void Stop()
        {
            _service.Stop();
            _service = null;
            _serviceLifetimeScope.Dispose();
            _serviceLifetimeScope = null;
        }
    }
}