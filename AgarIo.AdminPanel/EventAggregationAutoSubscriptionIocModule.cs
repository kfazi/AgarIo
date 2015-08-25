namespace AgarIo.AdminPanel
{
    using Autofac;
    using Autofac.Core;

    using Caliburn.Micro;

    public class EventAggregationAutoSubscriptionIocModule : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry registry, IComponentRegistration registration)
        {
            registration.Activated += OnComponentActivated;
        }

        private static void OnComponentActivated(object sender, ActivatedEventArgs<object> e)
        {
            var handler = e?.Instance as IHandle;
            if (handler != null)
            {
                e.Context.Resolve<IEventAggregator>().Subscribe(handler);
            }
        }
    }
}