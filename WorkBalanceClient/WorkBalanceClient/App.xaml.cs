namespace WorkBalanceClient
{
    using Content;
    using Infrastructure;
    using Infrastructure.Interfaces;
    using Logger;
    using Microsoft.Practices.Unity;
    using SoundAlarm;
    using System;
    using System.Windows;
    using System.Windows.Threading;
    using WelcomeScreen;

    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private readonly IUnityContainer iocContainer;
        private IWelcomeViewModel splashScreen;
        public App()
        {
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            iocContainer = new UnityContainer();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (splashScreen != null)
            {
                splashScreen.Dispose();
            }
            iocContainer.Resolve<ILogger>().Debug("WBC wird beendet");
        }

        private void HandleUnhandledExc(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //Log + Messag
            MessageBox.Show(e.Exception.Message + (e.Exception.InnerException == null ? "" : "\r\n IE: " + e.Exception), "Fatal error", MessageBoxButton.OK);
            e.Handled = false;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //this.ApplyTheme("DefaultTheme");
            RegisterModulesInBootstrapper(iocContainer);
            iocContainer.Resolve<ILogger>().Info("WBC gestartet");
            splashScreen = iocContainer.Resolve<IWelcomeViewModel>();
            splashScreen.NextStep("Starte...", 5);

            Start();
        }

        private void Start()
        {
            var future = DateTime.Now.AddSeconds(1);
            while (DateTime.Now < future)
            {
                //noop
            }
            MainWindow = iocContainer.Resolve<MainWindowViewModel>().Show();
            MainWindow.ContentRendered += MainWindowOnContentRendered;
        }

        private void MainWindowOnContentRendered(object sender, EventArgs eventArgs)
        {
            iocContainer.Resolve<AppController>().Run();
            splashScreen.Dispose();
        }

        private void RegisterModulesInBootstrapper(IUnityContainer container)
        {
            var bootstrapper = container.Resolve<Bootstrapper>();
            bootstrapper.RegisterModule(typeof(WelcomeModule))
                .RegisterModule(typeof(ContentModule))
                .RegisterModule(typeof(LoggerModule))
                //.RegisterModule(typeof(TimerTickerModule))
                .RegisterModule(typeof(SoundAlarmModule))
                ;
        }
    }
}
