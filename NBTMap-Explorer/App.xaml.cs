using Microsoft.Extensions.DependencyInjection;
using NBTMap_Explorer.Views;
using System.Windows;

namespace NBTMap_Explorer
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            var window = new BaseWindow();

            window.Show();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // Register your services here
            // Example: services.Add

        }
    }
}