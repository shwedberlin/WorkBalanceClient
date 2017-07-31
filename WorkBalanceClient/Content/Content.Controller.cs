namespace Content
{
    using System;
    using System.Reactive.Linq;

    using Microsoft.Practices.Unity;
    using Infrastructure.Interfaces;
    using WelcomeScreen;

    public partial class ContentViewModel : IContent
    {
        private readonly TimeSpan oneSecondTimeSpan = new TimeSpan(0, 0, 1);
        private readonly TimeSpan workExpirationTimeSpan = new TimeSpan(0, 0, 30);
        private readonly TimeSpan workTimeSpan = new TimeSpan(1, 0, 0);
        private readonly TimeSpan pauseTimeSpan = new TimeSpan(0, 15, 0);
        private TimeSpan WorkTimeSpan;
        private TimeSpan PauseTimeSpan;

        IDisposable timerSubscription;
        private IWelcomeViewModel splashScreen;

        public void Run()
        {
            InitializeViewModel();
            Observable
                .Interval(oneSecondTimeSpan)
                .ObserveOnDispatcher()
                .Subscribe(x => OnOneSecondTick());

            RunWorkTimer();
        }

        private void OnOneSecondTick()
        {
            // Updating the Label which displays the current date
            ClockDate = DateTime.Now.ToString("dd.MM.yyyy");

            // Updating the Label which displays the current time
            ClockTime = DateTime.Now.ToString("HH:mm:ss");
        }

        public event EventHandler<EventArgs> CloseRequested;
        internal void InvokeCloseRequested()
        {
            CloseRequested?.Invoke(this, new EventArgs());
        }

        public event EventHandler<EventArgs> WorktimeExpires;
        internal void InvokeWorktimeExpires()
        {
            WorktimeExpires?.Invoke(this, new EventArgs());
        }

        public event EventHandler<EventArgs> PauseStarted;
        internal void InvokePauseStarted()
        {
            PauseStarted?.Invoke(this, new EventArgs());
        }

        private void RunWorkTimer()
        {
            WorkTimeSpan = workTimeSpan;
            WorkTimer = WorkTimeSpan.ToString(@"hh\:mm\:ss");
            container.Resolve<ILogger>().Info("Arbeitszeit angefangen: " + WorkTimer);
            timerSubscription = Observable
                                    .Interval(oneSecondTimeSpan)
                                    .ObserveOnDispatcher()
                                    .Subscribe(x => OnWorkTimerTick());
        }

        private void RunPauseTimer()
        {
            PauseTimeSpan = pauseTimeSpan;
            InvokePauseStarted();
            WorkTimer = PauseTimeSpan.ToString(@"hh\:mm\:ss");
            container.Resolve<ILogger>().Info("Pause angefangen: " + WorkTimer);
            timerSubscription = Observable
                                    .Interval(oneSecondTimeSpan)
                                    .ObserveOnDispatcher()
                                    .Subscribe(x => OnPauseTimerTick());
        }

        private void OnWorkTimerTick()
        {
            WorkTimeSpan = WorkTimeSpan.Add(-oneSecondTimeSpan);
            WorkTimer = WorkTimeSpan.ToString(@"hh\:mm\:ss");

            if(WorkTimeSpan == workExpirationTimeSpan)
            {
                InvokeWorktimeExpires();
            }

            if (WorkTimeSpan < oneSecondTimeSpan)
            {
                splashScreen = container.Resolve<IWelcomeViewModel>();
                splashScreen.NextStep("Zeit für Pause!", 5);
                this.container.Resolve<ISoundAlarm>().Alarm();
                timerSubscription.Dispose();
                RunPauseTimer();
            }
        }

        private void OnPauseTimerTick()
        {
            PauseTimeSpan = PauseTimeSpan.Add(-oneSecondTimeSpan);
            WorkTimer = PauseTimeSpan.ToString(@"hh\:mm\:ss");
            splashScreen.NextStep("Zeit für Pause! "+ WorkTimer, 15);
            if (PauseTimeSpan < oneSecondTimeSpan)
            {
                container.Resolve<ISoundAlarm>().Notify();
                timerSubscription.Dispose();
                splashScreen.Dispose();
                RunWorkTimer();
            }
        }
    }
}
