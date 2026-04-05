using System.Windows;
using NBTMap_Explorer.ViewModels;

namespace NBTMap_Explorer.Views
{
    public partial class BaseWindow : Window
    {
        public BaseWindow()
        {
            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is BaseWindowViewModel viewModel)
            {
                viewModel.RequestClose += () => Close();
                viewModel.RequestMinimize += () => WindowState = WindowState.Minimized;
                viewModel.RequestMaximize += () =>
                {
                    WindowState = WindowState == WindowState.Maximized
                        ? WindowState.Normal
                        : WindowState.Maximized;
                };

                StateChanged += (s, ev) => viewModel.WindowState = WindowState;

                viewModel.WindowState = WindowState;
            }
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            Margin = WindowState == WindowState.Maximized 
                ? new Thickness(8) 
                : new Thickness(0);
        }
    }
}