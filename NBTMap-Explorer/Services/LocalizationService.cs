using System.Globalization;
using System.Resources;

namespace NBTMap_Explorer.Services
{
    public class LocalizationService
    {
        private const string Namespace = "NBTMap_Explorer";
        private const string LocalizationResourcesPath = $"{Namespace}.Resources.Strings";
        private ResourceManager _resourceManager;
        private CultureInfo _currentCulture;

        public LocalizationService()
        {
            _resourceManager = new ResourceManager(LocalizationResourcesPath, typeof(LocalizationService).Assembly);
            _currentCulture = CultureInfo.CurrentCulture;
        }

        public string GetString(string key)
        {
            if (_resourceManager is null) throw new SystemException("Ah yes, no resource manager instance, but how???");
            
            return _resourceManager.GetString(key, _currentCulture);
        }

        public void SetCulture(string cultureCode)
        {
            _currentCulture = new CultureInfo(cultureCode);
        }

    }
}
