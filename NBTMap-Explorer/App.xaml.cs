using Serilog;
using System.Windows;
using NBTMap_Explorer.Properties;
using System.IO;
using SplashScreen = NBTMap_Explorer.Views.SplashScreen;

namespace NBTMap_Explorer
{
    public partial class App : Application
    {
        private readonly string _logFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "NBTMap-Explorer",
            "log-.txt"
        );
        
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
                    _logFilePath,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: Settings.Default.SerilogStringTemplate
                )
                .CreateLogger();

            Log.Information("Application Starting");

            var splashScreen = new SplashScreen();

            splashScreen.Show();
        }
    }
}