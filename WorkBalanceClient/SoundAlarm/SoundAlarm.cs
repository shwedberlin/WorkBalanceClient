namespace SoundAlarm
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    using Infrastructure;
    using Infrastructure.Interfaces;

    using Microsoft.Practices.Unity;

    using NAudio.Wave;

    using Properties;

    class SoundAlarm : ISoundAlarm
    {
        private readonly CachedSound ErrorSound; 
        private readonly CachedSound WarningSound;
        private readonly CachedSound NotifySound;
        private readonly AudioPlaybackEngine playerEngine;

        public SoundAlarm(IUnityContainer container)
        {
            try
            {
                playerEngine = new AudioPlaybackEngine();
            }
            catch (System.Exception)
            {
                playerEngine = null;
            }
            float volume = 0.5f;
            

            ErrorSound = new CachedSound(@"Resources\err.wav", volume);
            WarningSound = new CachedSound(@"Resources\newsting.wav", volume);
            NotifySound = new CachedSound(@"Resources\notification.wav", volume);
        }

        public void Alarm(float volume)
        {
            if (playerEngine != null)
                playerEngine.PlaySound(@"Resources\err.wav", volume);          
        }

        public void Info()
        {
            if (playerEngine != null)
                playerEngine.PlaySound(WarningSound);
        }

        public void Notify()
        {
            if (playerEngine != null)
                playerEngine.PlaySound(NotifySound);
        }

        public void Alarm()
        {
            if (playerEngine != null)
                playerEngine.PlaySound(ErrorSound);
        }
    }
}
