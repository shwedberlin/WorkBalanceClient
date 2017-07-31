namespace Content
{
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Interaktionslogik für SimpleHeaderView.xaml
    /// </summary>
    public partial class ContentView
    {
        public ContentView()
        {
            InitializeComponent();
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var wnd = Window.GetWindow(this);
            if (wnd != null)
            {
                wnd.DragMove();
            }
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var wnd = Window.GetWindow(this);
            if (wnd == null)
            {
                return;
            }

            if (wnd.WindowState == WindowState.Maximized)
            {
                wnd.WindowState = WindowState.Normal;
            }
            else
            {
                wnd.WindowState = WindowState.Minimized;
            }
        }
    }
}
