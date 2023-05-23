namespace JobsApi.Dtos;

public record UserFilterDto(int? Page, int? PageSize, UserDtoType? Type);
