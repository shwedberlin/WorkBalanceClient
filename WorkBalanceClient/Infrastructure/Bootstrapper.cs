namespace Infrastructure
{
    using Infrastructure.Interfaces;
    using Microsoft.Practices.Unity;
    using System;

    public class Bootstrapper
    {
        private readonly IUnityContainer container;

        public Bootstrapper(IUnityContainer container)
        {
            this.container = container;
        }

        public Bootstrapper RegisterModule(Type moduleType)
        {
            IModule module = container.Resolve(moduleType) as IModule;
            if (module == null)
            {
                throw new ArgumentException("moduleType");
            }

            module.Register(container);
            return this;
        }
    }
}
