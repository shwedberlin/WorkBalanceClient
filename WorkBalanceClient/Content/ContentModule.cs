namespace Content
{
    using Infrastructure.Interfaces;
    using Microsoft.Practices.Unity;

    public class ContentModule : IModule
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<IContent, ContentViewModel>(new ContainerControlledLifetimeManager());
        }
    }
}
