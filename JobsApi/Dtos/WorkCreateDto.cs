namespace JobsApi.Dtos;

public record WorkCreateDto(string Title, string Description, DateTimeOffset StartAt, uint UserId, uint Value)
{
    public DateTimeOffset? EndAt { get; set; }
}