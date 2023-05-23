namespace JobsApi.Dtos;

public record JobCreateDto(uint Id, string Title, string Description, uint Value, JobDtoLevel Level, IEnumerable<JobSkillDto>? Skills);