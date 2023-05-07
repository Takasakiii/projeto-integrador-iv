namespace JobsApi.Dtos;

public record UserUpdateDto(string? ImageId, string? Description, uint? ExpectedValue, string? Role);