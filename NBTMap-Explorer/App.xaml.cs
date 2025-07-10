using NBTMap_Explorer.Views;
using System.Diagnostics;
using System.Windows;
using SplashScreen = NBTMap_Explorer.Views.SplashScreen;

namespace NBTMap_Explorer
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            if (Debugger.IsAttached)
            {
                BaseWindow baseWindow = new BaseWindow
                {
                };
                baseWindow.Show();
            }
            else
            {
                SplashScreen splashScreen = new SplashScreen
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Topmost = true
                };
                splashScreen.Show();
            }
        }
    }
}