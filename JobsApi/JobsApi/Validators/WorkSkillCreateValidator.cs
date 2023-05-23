using FluentValidation;
using JobsApi.Dtos;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Validators;

[Service(typeof(IValidator<WorkSkillCreateDto>))]
public class WorkSkillCreateValidator : AbstractValidator<WorkSkillCreateDto>
{
    public WorkSkillCreateValidator()
    {
        RuleFor(x => x.SkillId).NotEmpty();
        RuleFor(x => x.WorkId).NotEmpty();
    }
}