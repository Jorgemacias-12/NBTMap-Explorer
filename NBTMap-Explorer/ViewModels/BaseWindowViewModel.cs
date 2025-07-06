using NBTMap_Explorer.ViewModels.Interfaces;
using System.Windows;
using System.Windows.Input;

namespace NBTMap_Explorer.ViewModels
{
    public class BaseWindowViewModel : ViewModelBase
    {
        private WindowState _windowState;
        public WindowState WindowState
        {
            get => _windowState;
            set
            {
                if (_windowState != value)
                {
                    _windowState = value;
                    OnPropertyChanged(nameof(WindowState));
                    OnPropertyChanged(nameof(MaxRestoreIcon));
                }
            }
        }

        public string MaxRestoreIcon => WindowState == WindowState.Maximized ? "\uE923" : "\uE922";


        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand CloseCommand { get; }

        public Action? RequestClose { get; set; }
        public Action? RequestMinimize { get; set; }
        public Action? RequestMaximize { get; set; }

        public BaseWindowViewModel()
        {
            MinimizeCommand = new RelayCommand(_ => RequestMinimize?.Invoke());
            MaximizeCommand = new RelayCommand(_ => RequestMaximize?.Invoke());
            CloseCommand = new RelayCommand(_ => RequestClose?.Invoke());
        }
    }
}