using NBTMap_Explorer.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace NBTMap_Explorer.Views
{
    /// <summary>
    /// Lógica de interacción para SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();

            DataContext = new SplashScreenViewModel();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bool usesLeftButton = e.ChangedButton == MouseButton.Left;

            if (!usesLeftButton) return;

            DragMove();
        }
    }
}
