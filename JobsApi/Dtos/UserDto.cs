namespace JobsApi.Dtos;

public record UserDto(uint Id, string Name, UserDtoType Type, string? ImageId);