using Hudson.Models;

namespace Hudson.Tray.Presenters.EventHandlers
{
    /// <summary>
    /// Occurs when a <see cref="ServerModel"/>'s status changes
    /// </summary>
    public delegate void OnStatusChangeEventHandler(ServerModel model);

}
