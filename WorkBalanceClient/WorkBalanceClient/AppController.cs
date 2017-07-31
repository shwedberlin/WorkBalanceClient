namespace WorkBalanceClient
{
    using Content;
    using Infrastructure.Interfaces;
    using Microsoft.Practices.Unity;
    using System;
    using System.Windows;

    internal class AppController : IController
    {
        private readonly IUnityContainer container;
        public AppController(IUnityContainer container)
        {
            this.container = container;
        }

        public void Run()
        {
            //throw new NotImplementedException();
            StartContent();
        }

        private void StartContent()
        {
            var header = container.Resolve<IContent>();
            header.CloseRequested += CloseApp;
            header.WorktimeExpires += WorktimeExpiresSoon;
            header.PauseStarted += PauseStarted;
            header.Run();
        }

        private void PauseStarted(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Topmost = false;
        }

        private void WorktimeExpiresSoon(object sender, EventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Normal;
            Application.Current.MainWindow.Topmost=true;
            container.Resolve<ISoundAlarm>().Notify();
        }

        private void CloseApp(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
