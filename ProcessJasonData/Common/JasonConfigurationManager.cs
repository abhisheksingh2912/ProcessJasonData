using System.Configuration;

namespace ProcessJasonData.Common
{
    class JasonConfigurationManager : IConfigurationManager
    {
        public string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
