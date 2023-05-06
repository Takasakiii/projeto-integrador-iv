using FluentValidation;
using JobsApi.Dtos;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Validators;

[Service(typeof(IValidator<UserFilterDto>))]
public class UserFilterValidator : AbstractValidator<UserFilterDto>
{
    public UserFilterValidator()
    {
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(30);
    }
}