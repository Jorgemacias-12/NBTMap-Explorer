using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NBTMap_Explorer.Services;
using NBTMap_Explorer.ViewModels;
using System.Windows;

public partial class BaseWindowViewModel : ViewModelBase
{
    protected readonly LocalizationService _localizationService;

    [ObservableProperty]
    private string _title = "NBTMap Explorer";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MaxRestoreIcon))]
    private WindowState _windowState = WindowState.Normal;

    public string MaxRestoreIcon => WindowState == WindowState.Maximized ? "\uE923" : "\uE922";

    [RelayCommand]
    private void Minimize() => RequestMinimize?.Invoke();

    [RelayCommand]
    private void Maximize() => RequestMaximize?.Invoke();

    [RelayCommand]
    private void Close() => RequestClose?.Invoke();

    public Action? RequestClose { get; set; }
    public Action? RequestMinimize { get; set; }
    public Action? RequestMaximize { get; set; }

    public BaseWindowViewModel()
    {
        _localizationService = new LocalizationService();
    }

    public virtual void ChangeLanguage(string cultureCode)
    {
        _localizationService.SetCulture(cultureCode);
    }
}