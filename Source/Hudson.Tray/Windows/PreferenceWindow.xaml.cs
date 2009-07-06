using System;
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

            ServerService = (IServerService) Windsor.Instance.GetValue(typeof(IServerService));
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

        public bool Validate()
        {
            if (string.IsNullOrEmpty(Server.Text))
            {
                MessageBox.Show("No server URL entered.");

                return false;
            }

            // Automatically append HTTP
            if (!Server.Text.Contains("://")) Server.Text = "http://" + Server.Text;
            
            if (!Uri.IsWellFormedUriString(Server.Text, UriKind.Absolute))
            {
                MessageBox.Show("Invalid server URL: " + Server.Text);

                return false;
            }

            var success = false;

            // keep old settings
            var currentUserName = Settings.Default.Username;
            var currentPassword = Settings.Default.Password;

            // HACK: Save new settings 
            Settings.Default.Username = Username.Text;
            Settings.Default.Password = Password.Password;

            Settings.Default.Save();
            Settings.Default.Reload();

            try
            {
                var server = ServerService.GetServer(new Uri(Server.Text));

                if (server != null)
                {
                    success = true;
                }
                else
                {
                    MessageBox.Show("Unable to contact server.");
                }
            }
            catch (HudsonException ex)
            {
                MessageBox.Show("Unable to contact server: " + ex.Message);
            }
            

            // Restore old settings
            Settings.Default.Username = currentUserName;
            Settings.Default.Password = currentPassword;

            Settings.Default.Save();

            return success;
        }

        /// <summary>
        /// Handles the Click event of the Save control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate()) return;

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
            if (PollEveryLabel != null) PollEveryLabel.Content = Convert.ToInt32(PollEvery.Value) + " seconds";
        }
    }
}