namespace AgarIo.Server
{
    using AgarIo.Server.Infrastructure;

    using Autofac;

    using Topshelf;
    using Topshelf.HostConfigurators;

    public class Program
    {
        public static void Main()
        {
            var program = new Program();
            program.Run();
        }

        private void Run()
        {
            var autofacSetup = new AutofacSetup();
            var container = autofacSetup.Run();

            var autoMapperSetup = new AutoMapperSetup();
            autoMapperSetup.Run();

            HostFactory.Run(configurator => SetupService(configurator, container));
        }

        private void SetupService(HostConfigurator configurator, ILifetimeScope container)
        {
            configurator.AutofacService<IAgarIoService>(container, serviceConfigurator =>
            {
                serviceConfigurator.WhenStarted(s => s.Start());
                serviceConfigurator.WhenStopped(s => s.Stop());
            });
            configurator.SetServiceName("AgarIo.Service");
            configurator.SetDisplayName("AgarIo Service");
            configurator.SetDescription("Serves AgarIo Game");
            configurator.UseNLog();
        }
    }
}
