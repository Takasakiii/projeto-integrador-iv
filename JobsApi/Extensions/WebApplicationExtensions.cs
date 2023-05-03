namespace JobsApi.Extensions;

public static class WebApplicationExtensions
{
    public static void UseAppCors(this WebApplication application)
    {
        application.UseCors(x => x
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .WithExposedHeaders("x-total-items")
        );
    }
}