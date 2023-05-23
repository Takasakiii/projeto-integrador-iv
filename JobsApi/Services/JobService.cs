using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Exceptions;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using JobsApi.Services.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Services;

[Service(typeof(IJobService))]
public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IJobSkillService _jobSkillService;

    public JobService(IJobRepository jobRepository, IUnitOfWork unitOfWork, IMapper mapper, IJobSkillService jobSkillService)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jobSkillService = jobSkillService;
    }

    public async Task<JobDto> Create(JobCreateDto jobCreate, uint userId)
    {
        var model = await _jobRepository.GetById(jobCreate.Id) ?? _mapper.Map<JobModel>(jobCreate);

        if (model is null)
            throw new NotFoundException("Job", jobCreate.Id);

        model = _mapper.Map(jobCreate, model);
        
        model.UserId = userId;

        if (model.Id == 0)
        {
            await _jobRepository.Add(model);
        }
        else
        {
            _jobRepository.Update(model);
        }

        await _unitOfWork.SaveChanges();

        if (jobCreate.Skills != null)
        {
            await _jobSkillService.Create(jobCreate.Skills, model.Id);
        }

        return _mapper.Map<JobDto>(model);
    }

    public async ValueTask Delete(uint id, uint userId)
    {
        var model = await _jobRepository.GetById(id);
        
        if (model is null)
            throw new NotFoundException("Job", id);

        if (model.UserId != userId)
            throw new PermissionException("No permission to alter this user");

        await _jobSkillService.Clear(id);
        
        _jobRepository.Delete(model);
        await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<JobDto>> List()
    {
        var models = await _jobRepository.ListWithIncludes();

        return _mapper.Map<IEnumerable<JobDto>>(models);
    }

    public async Task<IEnumerable<JobLevelCountDto>> GetJobLevelCount()
    {
        var result = await _jobRepository.GetJobLevelCount();

        return result.OrderByDescending(x => x.Count);
    }
}