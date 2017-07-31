namespace SoundAlarm
{
    using Infrastructure.Interfaces;

    using Microsoft.Practices.Unity;

    public class SoundAlarmModule : IModule
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<ISoundAlarm, SoundAlarm>(new ContainerControlledLifetimeManager());
        }
    }
}
