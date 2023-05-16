using AutoMapper;
using FluentValidation;
using JobsApi.Dtos;
using JobsApi.Exceptions;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using JobsApi.Services.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Services;

[Service(typeof(IWorkService))]
public class WorkService : IWorkService
{
    private readonly IWorkRepository _workRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<WorkCreateDto> _workCreateValidator;
    private readonly IWorkSkillService _workSkillService;

    public WorkService(IWorkRepository workRepository, IUnitOfWork unitOfWork, IMapper mapper,
        IValidator<WorkCreateDto> workCreateValidator, IWorkSkillService workSkillService)
    {
        _workRepository = workRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _workCreateValidator = workCreateValidator;
        _workSkillService = workSkillService;
    }

    public async Task<WorkDto> Create(WorkCreateDto workCreate, uint userId)
    {
        await _workCreateValidator.ValidateAndThrowAsync(workCreate);

        var model = await _workRepository.GetById(workCreate.Id) ?? _mapper.Map<WorkModel>(workCreate);

        if (model is null)
            throw new NotFoundException("Work", workCreate.Id);

        model = _mapper.Map(workCreate, model);
        
        model.UserId = userId;

        if (model.Id == 0)
        {
            await _workRepository.Add(model);
        }
        else
        {
            _workRepository.Update(model);
        }
        
        await _unitOfWork.SaveChanges();

        if (workCreate.Skills is not null) 
            await _workSkillService.Create(workCreate.Skills, model.Id);
        
        return _mapper.Map<WorkDto>(model);
    }

    public async Task<WorkDto> GetById(uint id)
    {
        var work = await _workRepository.GetById(id);

        if (work is null)
            throw new NotFoundException("Work", id);

        return _mapper.Map<WorkDto>(work);
    }

    public async Task<IEnumerable<WorkDto>> List(WorkFilterDto workFilter)
    {
        var works = await _workRepository.List(workFilter);

        return _mapper.Map<IEnumerable<WorkDto>>(works);
    }
}