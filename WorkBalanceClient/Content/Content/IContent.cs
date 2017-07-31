namespace Content
{
    using System;
    using Infrastructure.Interfaces;

    public interface IContent : IController
    {
        /// <summary>
        /// Tritt auf wenn CloseButton gecklickt wird
        /// </summary>
        event EventHandler<EventArgs> CloseRequested;

        /// <summary>
        /// Tritt auf wenn Arbeitszeit bald abläuft
        /// </summary>
        event EventHandler<EventArgs> WorktimeExpires;

        /// <summary>
        /// Tritt auf wenn Pause anfängt
        /// </summary>
        event EventHandler<EventArgs> PauseStarted;
    }
}
