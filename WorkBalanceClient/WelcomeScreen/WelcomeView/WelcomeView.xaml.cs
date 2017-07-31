using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace WelcomeScreen
{
    [Flags]
    internal enum AnimationElement
    {
        None = 0x0,
        Bit0 = 0x1,
        Bit1 = 0x2,
        Bit2 = 0x4,
        Bit3 = 0x8,
        Bit4 = 0x10
    }

    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class WelcomeView
    {
        private const double ANIMATION_TIME = 0.3;
        private DispatcherTimer animationTimer;
        private readonly Dictionary<ushort, AnimationElement> AnimationSteps = new Dictionary<ushort, AnimationElement>
        {
            {0,AnimationElement.None},
            {1,AnimationElement.Bit0},
            {2,AnimationElement.Bit0 | AnimationElement.Bit1},
            {3,AnimationElement.Bit0 | AnimationElement.Bit1 | AnimationElement.Bit2},
            {4,AnimationElement.Bit0 | AnimationElement.Bit1 | AnimationElement.Bit2 | AnimationElement.Bit3},
            {5,AnimationElement.Bit0 | AnimationElement.Bit1 | AnimationElement.Bit2 | AnimationElement.Bit3 | AnimationElement.Bit4},
            {6,                        AnimationElement.Bit1 | AnimationElement.Bit2 | AnimationElement.Bit3 | AnimationElement.Bit4},
            {7,                                                AnimationElement.Bit2 | AnimationElement.Bit3 | AnimationElement.Bit4},
            {8,                                                                        AnimationElement.Bit3 | AnimationElement.Bit4},
            {9,                                                                                                AnimationElement.Bit4}
        }; 

        public WelcomeView()
        {
            InitializeComponent();
            AnimateProgress();
        }

        private void AnimateProgress()
        {
            animationTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(ANIMATION_TIME)};
            ushort animationTicker = 0;
            animationTimer.Tick += (sender, args) =>
            {
                animationTicker++;
                if (animationTicker > 9)
                {
                    animationTicker = 0;
                }
                System.Diagnostics.Trace.WriteLine(animationTicker.ToString() + ": " + AnimationSteps[animationTicker].ToString());
                LightOn(AnimationSteps[animationTicker]);
            };
            animationTimer.Start();
        }

        private void LightOn(AnimationElement element)
        {
            P1.Visibility = (element & AnimationElement.Bit0) == AnimationElement.Bit0 ? Visibility.Visible : Visibility.Hidden;
            P2.Visibility = (element & AnimationElement.Bit1) == AnimationElement.Bit1 ? Visibility.Visible : Visibility.Hidden;
            P3.Visibility = (element & AnimationElement.Bit2) == AnimationElement.Bit2 ? Visibility.Visible : Visibility.Hidden;
            P4.Visibility = (element & AnimationElement.Bit3) == AnimationElement.Bit3 ? Visibility.Visible : Visibility.Hidden;
            P5.Visibility = (element & AnimationElement.Bit4) == AnimationElement.Bit4 ? Visibility.Visible : Visibility.Hidden;
        }

        internal void StopAnimation()
        {
            animationTimer.Stop();
        }
    }

    
}
