using AutoMapper;
using FluentValidation;
using JobsApi.Configs;
using JobsApi.Dtos;
using JobsApi.Exceptions;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;
    private readonly IValidator<UserCreateDto> _userCreateValidator;
    private readonly IValidator<UserFilterDto> _userFilterValidator;
    private readonly IPaginationService _paginationService;

    public UserService(IUserRepository userRepository, IAppConfig appConfig, IUnitOfWork unitOfWork, IMapper mapper,
        IJwtService jwtService, IValidator<UserCreateDto> userCreateValidator,
        IValidator<UserFilterDto> userFilterValidator, IPaginationService paginationService)
    {
        _userRepository = userRepository;
        _appConfig = appConfig;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtService = jwtService;
        _userCreateValidator = userCreateValidator;
        _userFilterValidator = userFilterValidator;
        _paginationService = paginationService;
    }

    public async ValueTask CreateAdminUser()
    {
        var model = await _userRepository.GetByEmail("admin@jobs.com");

        if (model is null)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(_appConfig.Admin.Password);
            await _userRepository.Add(new UserModel("Admin", "admin@jobs.com", UserModelType.Admin, password));
            await _unitOfWork.SaveChanges();
        }
    }

    public async Task<JwtDto> Login(LoginDto login)
    {
        var model = await _userRepository.GetByEmail(login.Email);

        if (model is null)
            throw new AuthException("User is not found");

        if (!BCrypt.Net.BCrypt.Verify(login.Password, model.Password))
            throw new AuthException("Password is not match");

        var dto = _mapper.Map<UserDto>(model);
        var token = _jwtService.GenerateUserToken(dto);

        return new JwtDto(token);
    }

    public async Task<UserDto> GetById(uint id)
    {
        var model = await _userRepository.GetById(id);

        if (model is null)
            throw new NotFoundException("User", id);

        return _mapper.Map<UserDto>(model);
    }

    public async Task<UserDto> Create(UserCreateDto userCreate)
    {
        await _userCreateValidator.ValidateAndThrowAsync(userCreate);

        var model = _mapper.Map<UserModel>(userCreate);
        model.Password = BCrypt.Net.BCrypt.HashPassword(userCreate.Password);

        await _userRepository.Add(model);
        await _unitOfWork.SaveChanges();

        return _mapper.Map<UserDto>(model);
    }

    public async Task<IEnumerable<UserDto>> List(UserFilterDto userFilter)
    {
        await _userFilterValidator.ValidateAndThrowAsync(userFilter);
        
        var (users, count) = await _userRepository.Filter(userFilter);
        
        _paginationService.SetCount(count);

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}