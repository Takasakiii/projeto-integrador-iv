using FluentValidation;
using JobsApi.Dtos;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Validators;

[Service(typeof(IValidator<UserSkillCreateDto>))]
public class UserSkillCreateValidator : AbstractValidator<UserSkillCreateDto>
{
    public UserSkillCreateValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.SkillId).NotEmpty();
        RuleFor(x => x.Level).IsInEnum();
        RuleFor(x => x.Years).NotEmpty();
    }
}