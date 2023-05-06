namespace JobsApi.Dtos;

public record UserDto(uint Id, string Name, UserDtoType Type, string? ImageId, string? Description, uint? ExpectedValue,
    string? Role);