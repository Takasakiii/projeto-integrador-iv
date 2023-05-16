using FluentValidation;
using JobsApi.Dtos;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Validators;

[Service(typeof(IValidator<WorkCreateDto>))]
public class WorkCreateValidator : AbstractValidator<WorkCreateDto>
{
    public WorkCreateValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty()
            .MaximumLength(255);
        RuleFor(x => x.Value).NotEmpty();
        RuleFor(x => x.StartAt).NotEmpty();
    }
}