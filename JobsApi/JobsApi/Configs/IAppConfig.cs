namespace JobsApi.Configs;

public interface IAppConfig
{
    public IDatabaseConfig Database { get; }
    public IJwtConfig Jwt { get; }
}