namespace JobsApi.Dtos;

public record UserCreateDto(
    string Name,
    string Email,
    string Password,
    string RepeatPassword,
    UserDtoType Type
);