using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface ISkillService
{
    Task<SkillDto> GetById(uint id);
    Task<SkillDto> Create(SkillCreateDto skillCreate);
}