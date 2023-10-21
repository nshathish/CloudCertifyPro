using Api.EndPoints.Authentication.Dtos;
using FluentValidation;

namespace Api.EndPoints.Authentication.Validations;

public class LoginUserDtoValidation : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}