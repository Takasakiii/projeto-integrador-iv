using FluentValidation;
using JobsApi.Dtos;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Validators;

[Service(typeof(IValidator<SkillCreateDto>))]
public class SkillCreateValidator : AbstractValidator<SkillCreateDto>
{
    public SkillCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
    }
}