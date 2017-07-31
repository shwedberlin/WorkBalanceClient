namespace Logger
{
    using Infrastructure.Interfaces;
    using Microsoft.Practices.Unity;

    public class LoggerModule : IModule
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<ILogger, Log4NetLogger>(new ContainerControlledLifetimeManager());
        }
    }
}
