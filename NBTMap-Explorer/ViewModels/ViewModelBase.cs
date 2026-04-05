using CommunityToolkit.Mvvm.ComponentModel;

namespace NBTMap_Explorer.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        protected virtual void OnInitialized() { }
    }
}
