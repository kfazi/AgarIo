namespace AgarIo.AdminPanel
{
    using System;
    using System.Reflection;
    using System.Windows;

    using AgarIo.AdminPanel.ViewModels;

    using Autofac;

    using Caliburn.Micro;
    using Caliburn.Micro.Autofac;

    public class AppBootstrapper : AutofacBootstrapper<MainViewModel>
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
              .Where(type => type.Name.EndsWith("ViewModel"))
              .AsSelf()
              .AsImplementedInterfaces()
              .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => type.Name.EndsWith("View") || type.Name.EndsWith("Page"))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterModule<EventAggregationAutoSubscriptionIocModule>();
            builder.Register(context => AppSettings.Create()).AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<Connection>().As<IConnection>().SingleInstance();
        }

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
            EnforceNamespaceConvention = false;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            Application.ShutdownMode = ShutdownMode.OnMainWindowClose;

            DisplayRootViewFor<MainViewModel>();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            // Bug in AutofacBootstrapper
            Container.Dispose();
        }
    }
}