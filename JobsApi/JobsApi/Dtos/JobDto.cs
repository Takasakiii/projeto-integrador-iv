namespace JobsApi.Dtos;

public record JobDto(uint Id, string Title, string Description, uint Value, uint UserId, JobDtoLevel Level,
    IEnumerable<JobSkillDto>? Skills)
{
    public string? CompanyName { get; set; }
}