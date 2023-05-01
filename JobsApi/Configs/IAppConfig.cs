namespace JobsApi.Configs;

public interface IAppConfig
{
    public IDatabaseConfig Database { get; }
    public IJwtConfig Jwt { get; }
    public IAdminConfig Admin { get; set; }
}