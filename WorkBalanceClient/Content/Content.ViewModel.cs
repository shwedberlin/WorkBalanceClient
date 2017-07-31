namespace Content
{
    using Microsoft.Practices.Unity;
    using System.Windows.Input;

    using Infrastructure;
    using Infrastructure.Interfaces;

    public partial class ContentViewModel : ViewModel
    {
        //private readonly DispatcherTimer clockTimer = new DispatcherTimer();

        internal void InitializeViewModel()
        {
            AppTitle = "LVS Client";

            appCloseCommand = new CommandModel(OnCloseRequested, CanClose);

            mainRegion.Context = this;
        }

        #region Properties
        public string AppTitle
        {
            get;
            private set;
        }

        private bool newVersionAvailable;
        public bool NewVersionAvailable
        {
            get { return newVersionAvailable; }
            set { SetField(ref newVersionAvailable, value, "NewVersionAvailable"); }
        }

        private string clockDate;
        public string ClockDate
        {
            get { return clockDate; }
            set { SetField(ref clockDate, value, "ClockDate"); }
        }

        private string clockTime;
        public string ClockTime
        {
            get { return clockTime; }
            set { SetField(ref clockTime, value, "ClockTime"); }
        }

        private string workTimer;
        public string WorkTimer
        {
            get { return workTimer; }
            set { SetField(ref workTimer, value, "WorkTimer"); }
        }
        #endregion

        #region CloseApp Command
        private ICommand appCloseCommand;
        public ICommand AppCloseCommand
        {
            get
            {
                return appCloseCommand;
            }
        }

        private void OnCloseRequested(object parameter)
        {
            InvokeCloseRequested();
        }

        private bool CanClose(object parameter)
        {
            return true;
        }
        #endregion //Close App Command        
    }
}
