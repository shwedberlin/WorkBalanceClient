namespace Content
{
    using System;
    using System.Reactive.Linq;

    using Microsoft.Practices.Unity;
    using Infrastructure.Interfaces;
    using Infrastructure;

    public partial class ContentViewModel
    {
        private readonly IUnityContainer container;
        private readonly IRegion mainRegion;

        public ContentViewModel(IUnityContainer container, [Dependency(RegionNames.ContentRegion)] IRegion mainRegion)
        {
            this.container = container;
            this.mainRegion = mainRegion;           
        }
    }
}
