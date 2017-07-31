namespace Infrastructure.Interfaces
{
    using Microsoft.Practices.Unity;

    public interface IModule
    {
        void Register(IUnityContainer container);
    }
}
