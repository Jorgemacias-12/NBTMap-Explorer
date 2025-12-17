using Serilog;
using NBTMap_Explorer.Views;
using System.Diagnostics;
using System.Windows;
using SplashScreen = NBTMap_Explorer.Views.SplashScreen;

namespace NBTMap_Explorer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] <{SourceContext}> (T:{ThreadId}/P:{ProcessId}) {Message:lj}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .WriteTo.Console(outputTemplate: outputTemplate)
                .WriteTo.Debug(outputTemplate: outputTemplate)
                .WriteTo.File("logs/log-.txt",
                  rollingInterval: RollingInterval.Day,
                  outputTemplate: outputTemplate
                )
                .CreateLogger();

            Log.Information("Application started sucessfully!");

            if (Debugger.IsAttached)
            {
                BaseWindow baseWindow = new BaseWindow
                {
                };
                baseWindow.Show();
            }
            else
            {
                SplashScreen splashScreen = new()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Topmost = true
                };

                splashScreen.Show();
            }
        }
    }
}