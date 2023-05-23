namespace JobsApi.Dtos;

public record UserSkillCreateDto(uint Id, string Skill, UserSkillDtoLevel Level, uint Years);