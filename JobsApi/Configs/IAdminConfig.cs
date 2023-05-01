using Config.Net;

namespace JobsApi.Configs;

public interface IAdminConfig
{
    [Option(DefaultValue = "admin")]
    public string Password { get; }
}