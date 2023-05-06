namespace JobsApi.Dtos;

public record WorkDto(uint Id, string Title, string Description, DateTimeOffset StartAt, uint UserId, uint Value,
    DateTimeOffset? EndAt);