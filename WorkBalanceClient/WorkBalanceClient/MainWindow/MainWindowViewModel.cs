namespace WorkBalanceClient
{
    using Infrastructure;
    using Infrastructure.Interfaces;
    using Microsoft.Practices.Unity;
    using System.Windows;

    class MainWindowViewModel : ViewModel
    {
        private readonly IRegion contentRegion;
        private readonly IUnityContainer container;
        public IRegion ContentRegion
        {
            get { return contentRegion; }
        }

        public MainWindowViewModel(IUnityContainer container)
        {
            contentRegion = new RegionModel();

            this.container = container;

            // Define UI regions
            this.container.RegisterInstance<IRegion>(RegionNames.ContentRegion, contentRegion, new ContainerControlledLifetimeManager());

            // Define default region
            this.container.RegisterInstance<IRegion>(contentRegion, new ContainerControlledLifetimeManager());
        }

        public Window Show()
        {
            var mWindow = new MainWindow { DataContext = this };
            mWindow.Show();
            return mWindow;
        }
    }
}
