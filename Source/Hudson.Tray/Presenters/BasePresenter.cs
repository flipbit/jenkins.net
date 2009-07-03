using System;
using System.Threading;
using System.Timers;
using System.Windows;
using Hudson.Interfaces.Services;
using Hudson.Models;
using Hudson.Tray.Presenters.EventHandlers;
using Hudson.Tray.Properties;
using Hudson.Tray.Windows;
using Timer = System.Timers.Timer;

namespace Hudson.Tray.Presenters
{
    /// <summary>
    /// The base presenter that all presenters inherit from.
    /// </summary>
    public abstract class BasePresenter : IDisposable
    {
        #region Private

        protected StatusWindow statusWindow;

        private readonly Timer timer;

        private bool autoShownPreferences;

        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            CurrentPoll++;

            if (CurrentPoll <= MaximumPoll) return;

            // Reset the current poll count
            CurrentPoll = 0;

            timer.Stop();

            // Poll Hudson
            Poll();

            // Restart the timer
            timer.Start();
        }

        #endregion

        #region Service Properties

        /// <summary>
        /// Gets or sets the server service.
        /// </summary>
        /// <value>The server service.</value>
        public IServerService ServerService { get; set; }

        /// <summary>
        /// Gets or sets the current poll.
        /// </summary>
        /// <value>The current poll.</value>
        public int CurrentPoll { get; set; }

        /// <summary>
        /// Gets or sets the maximum poll.
        /// </summary>
        /// <value>The maximum poll.</value>
        public int MaximumPoll { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public ServerModel Model { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the build status changes.
        /// </summary>
        public event OnStatusChangeEventHandler OnStatusChange;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePresenter"/> class.
        /// </summary>
        protected BasePresenter()
        {
            timer = new Timer { Interval = 1000 };
            timer.Elapsed += Elapsed;

            Model = new ServerModel();

            // Load poll every x seconds setting
            MaximumPoll = Settings.Default.MaximumPoll;

            // Poll straight away
            CurrentPoll = MaximumPoll;

            timer.Start();
        }

        /// <summary>
        /// Fired every second by the <see cref="timer"/>.
        /// </summary>
        public void Poll()
        {
            Settings.Default.Reload();

            // Check server URL exists
            if (!Uri.IsWellFormedUriString(Settings.Default.Server, UriKind.Absolute))
            {
                // Show preferences window if first poll
                if (!autoShownPreferences)
                {
                    Application.Current.Dispatcher.BeginInvoke(new ThreadStart(ShowPreferences));
                }

                return;
            }

            // Get Settings
            var url = new Uri(Settings.Default.Server);

            MaximumPoll = Settings.Default.MaximumPoll;

            // Get the server data
            var server = ServerService.GetServer(url);

            // Update the model
            var updated = Model.Update(server);

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(ShowStatusWindow));

            if (!updated) return;

            // Fire updated event
            if (OnStatusChange != null) OnStatusChange(Model);
        }

        /// <summary>
        /// Shows the preferences window.
        /// </summary>
        public void ShowPreferences()
        {
            var enabled = timer.Enabled;

            if (enabled)
            {
                timer.Stop();
            }

            var preferenceWindow = new PreferenceWindow();

            preferenceWindow.ShowDialog();

            if (enabled)
            {
                timer.Start();
            }

            // The preferences window has been shown
            autoShownPreferences = true;
        }

        /// <summary>
        /// Shows the status window.
        /// </summary>
        protected void ShowStatusWindow()
        {
            if (statusWindow == null) statusWindow = new StatusWindow();

            statusWindow.Bind(Model);
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            timer.Stop();

            timer.Dispose();
        }
    }
}
