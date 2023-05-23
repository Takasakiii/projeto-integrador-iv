using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using JobsApi.Services.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Services;

[Service(typeof(IJobSkillService))]
public class JobSkillService : IJobSkillService
{
    private readonly IJobSkillRepository _jobSkillRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISkillService _skillService;

    public JobSkillService(IJobSkillRepository jobSkillRepository, IUnitOfWork unitOfWork, IMapper mapper,
        ISkillService skillService)
    {
        _jobSkillRepository = jobSkillRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _skillService = skillService;
    }

    public async ValueTask Create(IEnumerable<JobSkillDto> skills, uint jobId)
    {
        {
            var workSkills = await _jobSkillRepository.GetByJob(jobId);

            _jobSkillRepository.DeleteRange(workSkills);
            await _unitOfWork.SaveChanges();
        }

        foreach (var jobSkill in skills)
        {
            var skill = await _skillService.GetOrCreate(new SkillCreateDto(jobSkill.Skill));

            await _jobSkillRepository.Add(new JobSkillModel(jobId, skill.Id, jobSkill.Optional));
            await _unitOfWork.SaveChanges();
        }
    }

    public async ValueTask Clear(uint jobId)
    {
        var workSkills = await _jobSkillRepository.GetByJob(jobId);

        _jobSkillRepository.DeleteRange(workSkills);
        await _unitOfWork.SaveChanges();
    }
}