using JobsApi.Database;
using JobsApi.Extensions;
using JobsApi.Middlewares;
using JobsApi.Workers;
using Lina.DynamicMapperConfiguration;
using Lina.DynamicServicesProvider;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddDynamicServices<Program>();
builder.Services.AddDynamicMappers<Program>();
builder.Services.AddHostedService<DatabaseWorker>();

builder.Services.AddAppConfig();
builder.Services.AddAppJwt();
builder.Services.AddAppSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();