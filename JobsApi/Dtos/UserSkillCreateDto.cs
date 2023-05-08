namespace JobsApi.Dtos;

public record UserSkillCreateDto(uint UserId, uint SkillId, UserSkillDtoLevel Level, uint Years);