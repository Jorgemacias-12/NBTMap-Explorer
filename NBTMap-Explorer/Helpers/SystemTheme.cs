using Microsoft.Win32;
using NBTMap_Explorer.Properties;
using System.Windows;

namespace NBTMap_Explorer
{
    public class SystemTheme
    {
        private const string ThemeRegistryKeyPath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string ThemeRegistryValue = "AppsUseLightTheme";
        private static ResourceDictionary? _currentThemeDictionary;

        public static string? Theme { get; private set; } = null;

        public static string GetSystemTheme()
        {
            string savedTheme = Settings.Default.Theme;

            if (!string.IsNullOrEmpty(savedTheme))
            {
                return savedTheme;
            }

            return GetWindowsSystemTheme();
        }

        private static string GetWindowsSystemTheme()
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(ThemeRegistryKeyPath))
                {
                    bool isLightTheme =
                        key?
                        .GetValue(ThemeRegistryValue)
                        is
                        int registryValue
                        && registryValue > 0;

                    return isLightTheme ? "Light" : "Dark";
                }
            } 
            catch (Exception)
            {
                return "Light";
            }
        }

        public static void ApplyTheme(string theme)
        {
            var themeName = $"Theme.{theme}";
            var themeUri = new Uri($"Resources/{themeName}.xaml", UriKind.Relative);

            var newTheme = new ResourceDictionary
            {
                Source = themeUri
            };

            if (_currentThemeDictionary != null)
            {
                Application
                    .Current
                    .Resources
                    .MergedDictionaries
                    .Remove(_currentThemeDictionary);
            }

            Application
                .Current
                .Resources
                .MergedDictionaries
                .Add(newTheme);

            _currentThemeDictionary = newTheme;
        }

        public static void ApplyTheme()
        {
            if (string.IsNullOrEmpty(Theme)) return;


            var themeName = $"Theme.{Theme}";
            var themeUri = new Uri($"Resources/{themeName}.xaml", UriKind.Relative);

            var newTheme = new ResourceDictionary
            {
                Source = themeUri
            };

            if (_currentThemeDictionary != null)
            {
                Application
                    .Current
                    .Resources
                    .MergedDictionaries
                    .Remove(_currentThemeDictionary);
            }

            Application
                .Current
                .Resources
                .MergedDictionaries
                .Add(newTheme);

            _currentThemeDictionary = newTheme;
        }
    }
}