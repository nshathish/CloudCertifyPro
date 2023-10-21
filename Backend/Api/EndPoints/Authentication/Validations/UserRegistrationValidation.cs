using Api.EndPoints.Authentication.Dtos;
using FluentValidation;

namespace Api.EndPoints.Authentication.Validations;

public class UserRegistrationValidation:AbstractValidator<UserRegistrationDto>
{
public UserRegistrationValidation()
{
    RuleFor(x => x.FirstName)
        .NotEmpty()
        .MaximumLength(50);

    RuleFor(x => x.LastName)
        .NotEmpty()
        .MaximumLength(50);

    RuleFor(x => x.DateOfBirth)
        .NotEmpty();
    // .LessThan(DateTime.Today.AddYears(-18));

    RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress();

    RuleFor(x => x.Password)
        .NotEmpty()
        .MinimumLength(8)
        .MaximumLength(50);
}


}