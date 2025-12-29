using Serilog;
using System.Windows;
using SplashScreen = NBTMap_Explorer.Views.SplashScreen;
using NBTMap_Explorer.Properties;

namespace NBTMap_Explorer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .WriteTo.Console(outputTemplate: Settings.Default.SerilogStringTemplate)
                .WriteTo.Debug(outputTemplate: Settings.Default.SerilogStringTemplate)
                .WriteTo.File(
                    Environment.SpecialFolder.LocalApplicationData.ToString(),
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: Settings.Default.SerilogStringTemplate
                )
                .CreateLogger();

            var splashScreen = new SplashScreen();

            splashScreen.Show();
            Log.Information("Application Starting");
        }
    }
}