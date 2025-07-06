using NBTMap_Explorer.Views;
using System.Windows;
using SplashScreen = NBTMap_Explorer.Views.SplashScreen;

namespace NBTMap_Explorer
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            SplashScreen splashScreen = new SplashScreen();
            
            splashScreen.Show();
        }
    }
}