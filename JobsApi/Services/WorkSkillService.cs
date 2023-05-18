using AutoMapper;
using FluentValidation;
using JobsApi.Dtos;
using JobsApi.Exceptions;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using JobsApi.Services.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Services;

[Service(typeof(IWorkSkillService))]
public class WorkSkillService : IWorkSkillService
{
    private readonly IWorkSkillRepository _workSkillRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<WorkSkillCreateDto> _workSkillCreateValidator;
    private readonly ISkillService _skillService;

    public WorkSkillService(IWorkSkillRepository workSkillRepository, IUnitOfWork unitOfWork, IMapper mapper,
        IValidator<WorkSkillCreateDto> workSkillCreateValidator, ISkillService skillService)
    {
        _workSkillRepository = workSkillRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _workSkillCreateValidator = workSkillCreateValidator;
        _skillService = skillService;
    }

    public async Task<WorkSkillDto> GetById(uint id)
    {
        var model = await _workSkillRepository.GetById(id);

        if (model is null)
            throw new NotFoundException("WorkSkill", id);

        return _mapper.Map<WorkSkillDto>(model);
    }

    public async Task Create(IEnumerable<string> skills, uint workId)
    {
        {
            var workSkills = await _workSkillRepository.GetByWork(workId);

            _workSkillRepository.DeleteRange(workSkills);
            await _unitOfWork.SaveChanges();
        }

        foreach (var skillName in skills)
        {
            var skill = await _skillService.GetOrCreate(new SkillCreateDto(skillName));

            await _workSkillRepository.Add(new WorkSkillModel(workId, skill.Id));
            await _unitOfWork.SaveChanges();
        }
    }
}