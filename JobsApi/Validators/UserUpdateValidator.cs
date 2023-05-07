using FluentValidation;
using JobsApi.Dtos;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Validators;

[Service(typeof(IValidator<UserUpdateDto>))]
public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        RuleFor(x => x.Description).MaximumLength(255);
        RuleFor(x => x.ExpectedValue).NotEqual((uint?)0);
        RuleFor(x => x.Role).MaximumLength(100);
        RuleFor(x => x.ImageId).Length(32, 32);
    }
}