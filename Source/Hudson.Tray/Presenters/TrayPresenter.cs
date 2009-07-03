using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Hudson.Domain;
using Hudson.Models;
using Hudson.Tray.Presenters.Helper;
using Hudson.Tray.Properties;
using Hudson.Tray.Windows;

namespace Hudson.Tray.Presenters
{
    /// <summary>
    /// Presenter that displays notification through a <see cref="NotifyIcon"/>.
    /// </summary>
    public class TrayPresenter : BasePresenter
    {
        #region Private

        private readonly NotifyIcon notifyIcon;

        private bool firstUpdate;

        private void InitializeNotificationIcon()
        {
            InitializeNotificationIcon(null);
        }

        private void InitializeNotificationIcon(IEnumerable<JobModel> jobs)
        {
            notifyIcon.ContextMenu = new ContextMenu();

            // Show Status Window Menu Item
            var statusMenuItem = new MenuItem { Text = "Status" };
            notifyIcon.ContextMenu.MenuItems.Add(statusMenuItem);

            // Settings Menu Item
            var preferencesMenuItem = new MenuItem { Text = "Settings" };
            notifyIcon.ContextMenu.MenuItems.Add(preferencesMenuItem);

            // Seperator
            notifyIcon.ContextMenu.MenuItems.Add(new MenuItem { Text = "-", });

            if (jobs == null)
            {
                notifyIcon.ContextMenu.MenuItems.Add(new MenuItem { Text = "Jobs", Enabled = false });
            }
            else
            {
                foreach (var job in jobs)
                {
                    var jobMenuItem = new MenuItem { Text = job.Name, Tag = job.Url };

                    notifyIcon.ContextMenu.MenuItems.Add(jobMenuItem);

                    jobMenuItem.Click += ShowJobMenuClick;
                }
            }

            // Seperator
            notifyIcon.ContextMenu.MenuItems.Add(new MenuItem { Text = "-", });

            // Exit menu item
            var exitMenuItem = new MenuItem { Text = "Exit" };
            notifyIcon.ContextMenu.MenuItems.Add(exitMenuItem);

            // Assign events
            statusMenuItem.Click += ShowStatusMenuClick;
            preferencesMenuItem.Click += ShowPreferenceMenuClick;
            exitMenuItem.Click += ExitMenuClick;

            notifyIcon.MouseClick += NotifyIconMouseClick;

        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TrayPresenter"/> class.
        /// </summary>
        public TrayPresenter()
        {
            notifyIcon = new NotifyIcon
            {
                Text = "Hudson Monitor",
                Icon = Resources.Grey,
                Visible = true
            };

            InitializeNotificationIcon();

            OnStatusChange += TrayPresenterOnStatusChange;

            firstUpdate = true;
        }

        /// <summary>
        /// Trays the presenter on status change.
        /// </summary>
        /// <param name="model">The model.</param>
        void TrayPresenterOnStatusChange(ServerModel model)
        {
            notifyIcon.Icon = new IconFinder().Find(model.BuildStatus);
            notifyIcon.Text = model.Title;

            if (firstUpdate)
            {
                firstUpdate = false;

                InitializeNotificationIcon(model.Jobs);

                return;
            }

            switch (model.BuildStatus)
            {
                case BuildStatus.Building:
                    notifyIcon.ShowBalloonTip(2000, model.Title, model.Text, ToolTipIcon.Info);
                    break;

                case BuildStatus.Failed:
                    notifyIcon.ShowBalloonTip(2000, model.Title, model.Text, ToolTipIcon.Error);
                    break;

                case BuildStatus.Passed:
                    notifyIcon.ShowBalloonTip(2000, model.Title, model.Text, ToolTipIcon.Info);
                    break;

                default:
                    break;
            }

            
        }

        #region Events

        /// <summary>
        /// Displays the status window when the user clicks the tray icon
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        void NotifyIconMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowStatusWindow();
            }
        }

        /// <summary>
        /// Shows the <see cref="StatusWindow"/> when the user selects the menu item
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void ShowStatusMenuClick(object sender, EventArgs e)
        {
            statusWindow.Show();
        }

        /// <summary>
        /// Shows the <see cref="PreferenceWindow"/> when the user selects the menu item
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void ShowPreferenceMenuClick(object sender, EventArgs e)
        {
            ShowPreferences();
        }

        /// <summary>
        /// Shows the job menu click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void ShowJobMenuClick(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;

            var url = (Uri) menuItem.Tag;

            var proc = new Process { StartInfo = { FileName = url.ToString(), UseShellExecute = true } };
            
            proc.Start();
        }

        /// <summary>
        /// Exits the application when the user selects the menu item
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void ExitMenuClick(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            notifyIcon.Dispose();

            System.Windows.Application.Current.Shutdown();
        }

        #endregion
    }
}
