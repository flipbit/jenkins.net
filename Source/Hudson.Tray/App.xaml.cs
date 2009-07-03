using System.Windows;
using Hudson.Core;
using Hudson.Tray.Presenters;
using Microsoft.WindowsAPICodePack;

namespace Hudson.Tray
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private BasePresenter presenter;

        protected override void OnStartup(StartupEventArgs e)
        {
            if (CoreHelpers.RunningOnWin7)
            {
                presenter = (BasePresenter)Windsor.Instance.GetValue(typeof(SuperbarPresenter));
            }
            else
            {
                presenter = (BasePresenter)Windsor.Instance.GetValue(typeof(TrayPresenter));
            }

            ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            presenter.Dispose();
        }
    }
}
