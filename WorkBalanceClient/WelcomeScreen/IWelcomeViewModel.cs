namespace WelcomeScreen
{
    using System;

    public interface IWelcomeViewModel : IDisposable
    {
        string CurrentStep
        {
            get;
        }

        int CurrentProgress
        {
            get;
        }

        void NextStep(string name, int currentProgress);
        void NextStep(string name, int currentProgress, double pauseSeconds);
    }
}
