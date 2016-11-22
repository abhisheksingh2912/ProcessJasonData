/// <summary>
/// Interface to read Application configuration
/// </summary>
public interface IConfigurationManager
{
    string GetAppSetting(string key);
}