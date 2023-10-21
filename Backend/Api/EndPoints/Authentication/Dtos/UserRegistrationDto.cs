using System.ComponentModel.DataAnnotations;

namespace Api.EndPoints.Authentication.Dtos;

public class UserRegistrationDto
{
    [Required] public string FirstName { get; set; } = null!;
    [Required] public string LastName { get; set; } = null!;
    [Required] public DateOnly DateOfBirth { get; set; }
    [Required] public string Email { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}