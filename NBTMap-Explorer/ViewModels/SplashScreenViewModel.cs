using NBTMap_Explorer.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NBTMap_Explorer.ViewModels
{
    public class SplashScreenViewModel : ViewModelBase
    {
        private string _backgroundPatternSource;

        public string BackgroundPatternSource
        {
            get => _backgroundPatternSource;
            set
            {
                if (_backgroundPatternSource != value)
                {
                    _backgroundPatternSource = value;
                    OnPropertyChanged(nameof(BackgroundPatternSource));
                }
            }
        }

        private int progressValue;

        public int ProgressValue
        {
            get => progressValue;
            set
            {
                if (progressValue != value)
                {
                    progressValue = value;
                    OnPropertyChanged(nameof(ProgressValue));
                }
            }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged(nameof(StatusMessage));
                }
            }
        }

        public string AppVersion { get; }

        public SplashScreenViewModel()
        {
            var theme = Settings.Default.Theme;

            BackgroundPatternSource = theme == "Dark"
           ? "/Assets/Images/splash_screen_pattern_dark.png"
           : "/Assets/Images/splash_screen_pattern_light.png";


            AppVersion = $"v{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "Unknown"}";

            ProgressValue = 0;
            StatusMessage = "Initializing...";
        }
    }
}
