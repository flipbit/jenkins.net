using Hudson.Domain;

namespace Hudson.Tray.Presenters.EventHandlers
{
    /// <summary>
    /// Occurs when a <see cref="Build"/> is currently building.
    /// </summary>
    public delegate void OnBuildingEventHandler(int percent);
}
