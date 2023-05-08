using AutoMapper;
using FluentValidation;
using JobsApi.Dtos;
using JobsApi.Exceptions;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using JobsApi.Services.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Services;

[Service(typeof(ISkillService))]
public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<SkillCreateDto> _skillCreateValidator;

    public SkillService(ISkillRepository skillRepository, IUnitOfWork unitOfWork, IMapper mapper,
        IValidator<SkillCreateDto> skillCreateValidator)
    {
        _skillRepository = skillRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _skillCreateValidator = skillCreateValidator;
    }

    public async Task<SkillDto> Create(SkillCreateDto skillCreate)
    {
        await _skillCreateValidator.ValidateAndThrowAsync(skillCreate);

        var model = _mapper.Map<SkillModel>(skillCreate);

        await _skillRepository.Add(model);
        await _unitOfWork.SaveChanges();

        return _mapper.Map<SkillDto>(model);
    }

    public async Task<SkillDto> GetById(uint id)
    {
        var model = await _skillRepository.GetById(id);

        if (model is null)
            throw new NotFoundException("Skill", id);

        return _mapper.Map<SkillDto>(model);
    }
}