namespace AgarIo.Server
{
    using AgarIo.Server.AdminCommands;
    using AgarIo.Server.Connections;
    using AgarIo.Server.Logic;
    using AgarIo.Server.Logic.Physics;
    using AgarIo.Server.Logic.Physics.Chipmunk;
    using AgarIo.Server.PlayerCommands;
    using AgarIo.SystemExtension;

    using Autofac;

    using NodaTime;

    internal class AutofacSetup
    {
        public IContainer Run()
        {
            var builder = new ContainerBuilder();
            builder.Register(context => AppSettings.Create()).AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(context => SystemClock.Instance).As<IClock>();

            builder.RegisterType<ConnectionListener>().As<IConnectionListener>().InstancePerLifetimeScope();
            builder.RegisterType<AgarIoService>().As<IAgarIoService>().InstancePerLifetimeScope();
            builder.RegisterType<AdminCommandFactory>().As<IAdminCommandFactory>();
            builder.RegisterType<PlayerCommandFactory>().As<IPlayerCommandFactory>();
            builder.RegisterType<ConnectionFactory>().As<IConnectionFactory>();
            builder.RegisterType<RandomWrap>().As<IRandom>();
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ChipmunkPhysics>().As<IPhysics>().InstancePerLifetimeScope();
            builder.RegisterType<Game>().As<IGame>().InstancePerLifetimeScope();
            builder.RegisterType<StateTracker>().As<IStateTracker>().InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}