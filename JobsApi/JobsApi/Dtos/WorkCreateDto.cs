namespace JobsApi.Dtos;

public record WorkCreateDto(uint Id, string Title, string Description, DateTimeOffset StartAt, uint Value)
{
    public DateTimeOffset? EndAt { get; set; }
    public IEnumerable<string>? Skills { get; set; }
}