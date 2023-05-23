using Config.Net;

namespace JobsApi.Configs;

public interface IDatabaseConfig
{
    [Option(DefaultValue = "Server=localhost;Database=jobs;User Id=root;Password=root;")]
    public string Url { get; }
}