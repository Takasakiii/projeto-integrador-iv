namespace JobsApi.Dtos;

public record UserUpdateDto(string? Name, string? ImageId, string? Description, uint? ExpectedValue, string? Role);
