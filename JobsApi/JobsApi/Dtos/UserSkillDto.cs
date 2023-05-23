namespace JobsApi.Dtos;

public record UserSkillDto(uint Id, uint UserId, uint SkillId, UserSkillDtoLevel Level, uint Years)
{
    public string? Skill { get; set; }
}