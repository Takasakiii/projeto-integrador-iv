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
    private readonly IValidator<UserUpdateDto> _userUpdateValidator;
    private readonly IValidator<UserSkillCreateDto> _userSkillCreateValidator;
    private readonly ISkillService _skillService;
    private readonly IUserSkillService _userSkillService;

    public UserService(IUserRepository userRepository, IAppConfig appConfig, IUnitOfWork unitOfWork, IMapper mapper,
        IJwtService jwtService, IValidator<UserCreateDto> userCreateValidator,
        IValidator<UserFilterDto> userFilterValidator, IPaginationService paginationService,
        IValidator<UserUpdateDto> userUpdateValidator, IValidator<UserSkillCreateDto> userSkillCreateValidator,
        ISkillService skillService, IUserSkillService userSkillService)
    {
        _userRepository = userRepository;
        _appConfig = appConfig;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtService = jwtService;
        _userCreateValidator = userCreateValidator;
        _userFilterValidator = userFilterValidator;
        _paginationService = paginationService;
        _userUpdateValidator = userUpdateValidator;
        _userSkillCreateValidator = userSkillCreateValidator;
        _skillService = skillService;
        _userSkillService = userSkillService;
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

    public async Task<UserDto> Update(UserUpdateDto userUpdate, uint userId, uint requestUserId)
    {
        await _userUpdateValidator.ValidateAndThrowAsync(userUpdate);

        if (userId != requestUserId)
            throw new PermissionException("No permission to update this user");

        var user = await _userRepository.GetById(userId);

        if (user is null)
            throw new NotFoundException("User", userId);

        user = _mapper.Map(userUpdate, user);

        _userRepository.Update(user);
        await _unitOfWork.SaveChanges();

        return _mapper.Map<UserDto>(user);
    }
}