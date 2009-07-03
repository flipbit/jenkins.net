using System.ComponentModel;
using System.Windows;
using Hudson.Models;

namespace Hudson.Tray.Windows
{
    /// <summary>
    /// Interaction logic for StatusWindow.xaml
    /// </summary>
    public partial class StatusWindow
    {
        /// <summary>
        /// Gets or sets a value indicating whether to hide or minimize on close.
        /// </summary>
        /// <value><c>true</c> if [hide on close]; otherwise, <c>false</c>.</value>
        public bool HideOnClose { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusWindow"/> class.
        /// </summary>
        public StatusWindow()
        {
            InitializeComponent();

            HideOnClose = true;
        }

        /// <summary>
        /// Binds the specified build.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Bind(ServerModel model)
        {           
            Jobs.ItemsSource = model.Jobs;

            Jobs.Items.Refresh();
        }

        /// <summary>
        /// Handles the Closing event of the StatusWindow control.
        /// Always ensure the application is kept running.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void StatusWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

            if (HideOnClose) Hide(); else WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Shows the preferences UI
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var preferenceWindow = new PreferenceWindow();

            preferenceWindow.ShowDialog();
        }

        /// <summary>
        /// Exits the application
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
