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

    public WorkService(IWorkRepository workRepository, IUnitOfWork unitOfWork, IMapper mapper,
        IValidator<WorkCreateDto> workCreateValidator)
    {
        _workRepository = workRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _workCreateValidator = workCreateValidator;
    }

    public async Task<WorkDto> Create(WorkCreateDto workCreate, uint userId)
    {
        await _workCreateValidator.ValidateAndThrowAsync(workCreate);

        if (workCreate.UserId != userId)
            throw new PermissionException("No permission to create for this user");

        var model = _mapper.Map<WorkModel>(workCreate);

        await _workRepository.Add(model);
        await _unitOfWork.SaveChanges();

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