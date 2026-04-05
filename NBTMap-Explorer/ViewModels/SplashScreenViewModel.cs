using NBTMap_Explorer.Properties;

namespace NBTMap_Explorer.ViewModels
{
    public class SplashScreenViewModel : BaseWindowViewModel
    {
        private string? _backgroundPatternSource;
        private int _progressValue;
        private string? _statusMessage;

        public string BackgroundPatternSource
        {
            get => _backgroundPatternSource ?? "";
            set
            {
                if (_backgroundPatternSource != value)
                {
                    _backgroundPatternSource = value;
                    OnPropertyChanged(nameof(BackgroundPatternSource));
                }
            }
        }

        public int ProgressValue
        {
            get => _progressValue;
            set
            {
                if (_progressValue != value)
                {
                    _progressValue = value;
                    OnPropertyChanged(nameof(ProgressValue));
                }
            }
        }

        public string StatusMessage
        {
            get => _statusMessage ?? "";
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged(nameof(StatusMessage));
                }
            }
        }

        public static string AppVersion
        {
            get
            {
                return $"v{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString(3)}" ?? "unknown";
            }
        }

        public string CloseButtonCaption => _localizationService.GetString("SplashScreen_CloseButtonCaption");
        public string ProgramName => _localizationService.GetString("SplashScreen_ProgramName");
        public string WindowTitle => _localizationService.GetString("SplashScreen_WindowTitle");
        public string VersionCaption => _localizationService.GetString("SplashScreen_VersionCaption");
        public static string Copyright => $"{DateTime.Today.Year} NBTMap-Explorer";

        public override void ChangeLanguage(string cultureCode)
        {
            base.ChangeLanguage(cultureCode);
        }

        public SplashScreenViewModel()
        {
            var theme = Settings.Default.Theme;

            BackgroundPatternSource = theme == "Dark"
               ? "/Assets/Images/splash_screen_pattern_dark.png"
               : "/Assets/Images/splash_screen_pattern_light.png";

            ProgressValue = 0;
        }
    }
}
