namespace Infrastructure
{
    using System.ComponentModel;
    using Interfaces;

    public static class RegionNames
    {
        public const string ContentRegion = "ContentRegion";
    }

    public class RegionModel : IRegion, INotifyPropertyChanged
    {
        private ViewModel context;
        public ViewModel Context
        {
            get { return context; }
            set
            {
                context = value;
                InvokePropertyChanged("Context");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
