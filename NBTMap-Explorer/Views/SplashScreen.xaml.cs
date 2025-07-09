using NBTMap_Explorer.Helpers;
using System.Windows;

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
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        bool isPreseed = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isPreseed = !isPreseed;

            string themeToApply = isPreseed ? "Dark" : "Light";

            SystemTheme.ApplyTheme(themeToApply);
        }
    }
}
