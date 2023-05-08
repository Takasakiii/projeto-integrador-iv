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
    private readonly IWorkService _workService;

    public WorkSkillService(IWorkSkillRepository workSkillRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<WorkSkillCreateDto> workSkillCreateValidator, IWorkService workService)
    {
        _workSkillRepository = workSkillRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _workSkillCreateValidator = workSkillCreateValidator;
        _workService = workService;
    }

    public async Task<WorkSkillDto> Create(WorkSkillCreateDto workSkillCreate, uint userId)
    {
        await _workSkillCreateValidator.ValidateAndThrowAsync(workSkillCreate);

        var work = await _workService.GetById(workSkillCreate.WorkId);

        if (work.UserId != userId)
            throw new PermissionException("No permission to change this work");

        var model = _mapper.Map<WorkSkillModel>(workSkillCreate);

        await _workSkillRepository.Add(model);
        await _unitOfWork.SaveChanges();

        return _mapper.Map<WorkSkillDto>(model);
    }

    public async Task<WorkSkillDto> GetById(uint id)
    {
        var model = await _workSkillRepository.GetById(id);

        if (model is null)
            throw new NotFoundException("WorkSkill", id);

        return _mapper.Map<WorkSkillDto>(model);
    }
}