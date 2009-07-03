using System.Windows;
using Hudson.Core;
using Hudson.Interfaces.Services;
using Hudson.Tray.Properties;

namespace Hudson.Tray.Windows
{
    /// <summary>
    /// Interaction logic for PreferenceWindow.xaml
    /// </summary>
    public partial class PreferenceWindow
    {
        /// <summary>
        /// Gets or sets the server service.
        /// </summary>
        /// <value>The server service.</value>
        public IServerService ServerService { get; set; }

        public PreferenceWindow()
        {
            InitializeComponent();

            ServerService = (IServerService)Windsor.Instance.GetValue(typeof(IServerService));
        }

        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Bind settings to window
            Server.Text = Settings.Default.Server;
            Username.Text = Settings.Default.Username;
            Password.Password = Settings.Default.Password;
        }

        /// <summary>
        /// Handles the Click event of the Save control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Bind settings to window
            Settings.Default.Server = Server.Text;
            Settings.Default.Username = Username.Text;
            Settings.Default.Password = Password.Password;

            Settings.Default.Save();

            Close();
        }

        /// <summary>
        /// Handles the Click event of the Cancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PollEvery_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PollEveryLabel != null) PollEveryLabel.Content = PollEvery.Value + " seconds";
        }
    }
}