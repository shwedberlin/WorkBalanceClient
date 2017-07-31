namespace WelcomeScreen
{
    using Infrastructure.Interfaces;
    using Microsoft.Practices.Unity;

    public class WelcomeModule : IModule
    {
        public void Register(IUnityContainer container)
        {
            // register view models
            container.RegisterType<IWelcomeViewModel, WelcomeViewModel>();
            
        }
    }
}
