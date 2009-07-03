using System.Threading;
using System.Timers;
using System.Windows;
using Hudson.Models;
using Hudson.Tray.Windows;
using Microsoft.WindowsAPICodePack.Shell.Taskbar;
using Timer = System.Timers.Timer;

namespace Hudson.Tray.Presenters
{
    /// <summary>
    /// Presenter to show a UI on the Windows 7 Superbar
    /// </summary>
    public class SuperbarPresenter : TrayPresenter
    {
        private readonly JumpList jumpList;

        private Timer timer;

        private bool firstPoll;

        public SuperbarPresenter()
        {
            timer = new Timer();

            Taskbar.AppID = "HudsonTray";

            jumpList = Taskbar.JumpList;

            firstPoll = true;

            OnStatusChange += SuperbarPresenterOnStatusChange;

            statusWindow = new StatusWindow { WindowState = WindowState.Minimized };

            statusWindow.Show();

            statusWindow.HideOnClose = false;
        }

        void SuperbarPresenterOnStatusChange(ServerModel model)
        {
            if (model.Percent > -1)
            {
                StartBuildTimer();
            }
            else
            {
                SetPercentage(-1);
            }

            if (!firstPoll) return;

            foreach (var job in model.Jobs)
            {
                jumpList.UserTasks.Add(new JumpListLink
                                           {
                                               Title = job.Name,
                                               Path = job.Url.ToString()
                                           });
            }

            jumpList.RefreshTaskbarList();

            firstPoll = false;
        }

        public void StartBuildTimer()
        {
            SetPercentage(Model.Percent);

            timer.Interval = 1000;

            timer.Elapsed += TimerElapsed;

            timer.Start();

        }

        void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            SetPercentage(Model.Percent);

            if (Model.Percent > 100) timer.Stop();
        }

        void SetPercentage(int percent)
        {
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(() => SetThreadedPercent(percent)));
        }

        void SetThreadedPercent(int percent)
        {
            System.Diagnostics.Debug.WriteLine("Percentage: " + percent);

            if (percent < 0)
            {
                Taskbar.ProgressBar.State = TaskbarButtonProgressState.Normal;
                Taskbar.ProgressBar.State = TaskbarButtonProgressState.NoProgress;
            }
            else if (percent < 101)
            {
                Taskbar.ProgressBar.State = TaskbarButtonProgressState.Normal;

                Taskbar.ProgressBar.MaxValue = 100;

                Taskbar.ProgressBar.CurrentValue = percent;
            }
            else
            {
                Taskbar.ProgressBar.State = TaskbarButtonProgressState.Indeterminate;
            }
        }
    }
}
