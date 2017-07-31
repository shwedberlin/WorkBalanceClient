namespace WelcomeScreen
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Threading;
    using Infrastructure;

    class WelcomeViewModel : ViewModel, IWelcomeViewModel
    {
        private readonly WelcomeView welcomeWindow;
        
        public WelcomeViewModel()
        {
            welcomeWindow = new WelcomeView {DataContext = this};
            welcomeWindow.Show();
        }

        public string Version
        {
            get
            {
                var productVersion = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).ProductVersion;
                return productVersion;
                //var version = Assembly.GetEntryAssembly().GetName().Version;
                //return version.ToString();
            }
        }

        private string someText = "";
        public String CurrentStep
        {
            get { return someText; }
            private set
            {
                SetField(ref someText, value, "CurrentStep");
            }
        }

        private int progress = 15;
        public int CurrentProgress
        {
            get { return progress; }
            private set
            {
                SetField(ref progress, value, "CurrentProgress");
            }
        }
 
        public void Dispose()
        {
            welcomeWindow.StopAnimation();
            welcomeWindow.Close();            
        }


        public void NextStep(string name, int currentProgress)
        {
            CurrentStep = name;
            CurrentProgress = currentProgress;
        }

        public void NextStep(string name, int currentProgress, double pauseSeconds)
        {
            CurrentStep = name;
            CurrentProgress = currentProgress;
            Wait(pauseSeconds);
        }

        //Warten ohne UI Thread zu blockieren
        private void Wait(double seconds)
        {
            var frame = new DispatcherFrame();
            new Thread((ThreadStart)(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                frame.Continue = false;
            })).Start();
            Dispatcher.PushFrame(frame);
        }
    }
}
