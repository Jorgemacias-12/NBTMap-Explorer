using Microsoft.Win32;
using NBTMap_Explorer.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NBTMap_Explorer.Helpers
{
    public class SystemTheme
    {
        private const string ThemeRegistryKeyPath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string ThemeRegistryValue = "AppsUseLightTheme";
        private static ResourceDictionary? _currentThemeDictionary;

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
                    if (key?.GetValue(ThemeRegistryValue) is int registryValue)
                    {
                        return registryValue > 0 ? "Light" : "Dark";
                    }
                }
            }
            catch (Exception)
            {
                return "Light";
            }

            return "Light";
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
                Application.Current.Resources.MergedDictionaries.Remove(_currentThemeDictionary);
            }

            Application.Current.Resources.MergedDictionaries.Add(newTheme);
            _currentThemeDictionary = newTheme;
        }
    }
}
