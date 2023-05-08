using FluentValidation;
using JobsApi.Dtos;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Validators;

[Service(typeof(IValidator<UserSkillFilterDto>))]
public class UserSkillFilterValidator : AbstractValidator<UserSkillFilterDto>
{
    public UserSkillFilterValidator()
    {
        RuleFor(x => x.UserId).NotEqual((uint?)0);
    }
}