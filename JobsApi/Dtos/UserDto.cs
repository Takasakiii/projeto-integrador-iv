namespace JobsApi.Dtos;

public record UserDto(uint Id, string Name, UserDtoType Type, string? ImageId, string? Description, uint? ExpectedValue,
    string? Role)
{
    private IEnumerable<UserSkillDto>? Skills { get; set; }
    public IEnumerable<WorkDto>? Works { get; set; }
    public IEnumerable<JobDto>? Jobs { get; set; }
}