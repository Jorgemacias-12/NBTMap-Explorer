using System.Windows;

namespace NBTMap_Explorer.Interfaces
{
    public interface IWindowCommands
    {
        Action? RequestClose { get; set; }
        Action? RequestMinimize { get; set; }
        Action? RequestMaximize { get; set; }

        WindowState WindowState { get; set; }
    }
}
