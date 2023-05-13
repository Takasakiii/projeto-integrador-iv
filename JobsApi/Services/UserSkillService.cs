using AutoMapper;
using FluentValidation;
using JobsApi.Dtos;
using JobsApi.Exceptions;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using JobsApi.Services.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Services;

[Service(typeof(IUserSkillService))]
public class UserSkillService : IUserSkillService
{
    private readonly IUserSkillRepository _userSkillRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<UserSkillCreateDto> _userSkillCreateValidator;
    private readonly IValidator<UserSkillFilterDto> _userSkillFilterValidator;
    private readonly ISkillService _skillService;

    public UserSkillService(IUserSkillRepository userSkillRepository, IUnitOfWork unitOfWork, IMapper mapper,
        IValidator<UserSkillCreateDto> userSkillCreateValidator,
        IValidator<UserSkillFilterDto> userSkillFilterValidator, ISkillService skillService)
    {
        _userSkillRepository = userSkillRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userSkillCreateValidator = userSkillCreateValidator;
        _userSkillFilterValidator = userSkillFilterValidator;
        _skillService = skillService;
    }

    public async Task<UserSkillDto> Create(UserSkillCreateDto userSkillCreate, uint userId)
    {
        await _userSkillCreateValidator.ValidateAndThrowAsync(userSkillCreate);

        var skill = await _skillService.GetOrCreate(new SkillCreateDto(userSkillCreate.Skill));

        var model = await _userSkillRepository.GetById(userSkillCreate.Id) ?? _mapper.Map<UserSkillModel>(userSkillCreate);

        if (model is null)
            throw new NotFoundException("UserSkill", userSkillCreate.Id);
        
        model = _mapper.Map(userSkillCreate, model);

        model.UserId = userId;
        model.SkillId = skill.Id;

        if (model.Id == 0)
        {
            await _userSkillRepository.Add(model);
        }
        else
        {
            _userSkillRepository.Update(model);
        }

        await _unitOfWork.SaveChanges();

        return _mapper.Map<UserSkillDto>(model);
    }

    public async Task<UserSkillDto> GetById(uint id)
    {
        var userSkill = await _userSkillRepository.GetById(id);

        if (userSkill is null)
            throw new NotFoundException("User Skill", id);

        return _mapper.Map<UserSkillDto>(userSkill);
    }

    public async Task<IEnumerable<UserSkillDto>> Filter(UserSkillFilterDto filter)
    {
        await _userSkillFilterValidator.ValidateAndThrowAsync(filter);

        var userSkills = await _userSkillRepository.Filter(filter);

        return _mapper.Map<IEnumerable<UserSkillDto>>(userSkills);
    }

    public async ValueTask Delete(uint id, uint userId)
    {
        var userSkill = await _userSkillRepository.GetById(id);

        if (userSkill is null)
            throw new NotFoundException("User Skill", id);

        if (userSkill.UserId != userId)
            throw new PermissionException("No permission to change this user");

        _userSkillRepository.Delete(userSkill);
        await _unitOfWork.SaveChanges();
    }

    public async Task<UserSkillDto?> GetByUserAndSkill(uint userId, uint skillId)
    {
        var model = await _userSkillRepository.GetByUserAndSkill(userId, skillId);
        return _mapper.Map<UserSkillDto?>(model);
    }
    
    
    public async Task<IEnumerable<UserSkillDto>> MostUsed()
    {
        var skills = await _userSkillRepository.GetMostUsed();

        return _mapper.Map<IEnumerable<UserSkillDto>>(skills);
    }
}