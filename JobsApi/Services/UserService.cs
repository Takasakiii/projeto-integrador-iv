using JobsApi.Configs;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using JobsApi.Services.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Services;

[Service(typeof(IUserService))]
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAppConfig _appConfig;
    private readonly IUnityOfWork _unityOfWork;

    public UserService(IUserRepository userRepository, IAppConfig appConfig, IUnityOfWork unityOfWork)
    {
        _userRepository = userRepository;
        _appConfig = appConfig;
        _unityOfWork = unityOfWork;
    }

    public async ValueTask CreateAdminUser()
    {
        var model = await _userRepository.GetByEmail("admin@jobs.com");

        if (model is null)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(_appConfig.Admin.Password);
            await _userRepository.Add(new UserModel("Admin", "admin@jobs.com", UserModelType.Admin, password));
            await _unityOfWork.SaveChanges();
        }
    }
}