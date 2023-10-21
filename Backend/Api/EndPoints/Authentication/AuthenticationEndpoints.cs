using Api.EndPoints.Authentication.Dtos;
using Api.EndPoints.Authentication.ViewModels;
using Api.Infrastructure.Configurations;
using Api.Infrastructure.Models;
using AutoMapper;
using Data.Account;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Api.EndPoints.Authentication;

public static class AuthenticationEndpoints
{
    public static RouteGroupBuilder MapAuthenticationApi(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost("/register", RegisterUser)
            .WithName(nameof(RegisterUser));

        groupBuilder.MapPost("/login", LoginUser)
            .WithName(nameof(LoginUser));

        return groupBuilder;
    }

    private static async Task<Results<BadRequest<ApiResponse>, Ok<ApiResponse<UserRegistrationVm>>>> RegisterUser(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IValidator<UserRegistrationDto> validator,
        IOptions<JwtConfig> options,
        [FromBody] UserRegistrationDto model)
    {
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
            return TypedResults.BadRequest(new ApiResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            });

        var userExists = await userManager.FindByEmailAsync(model.Email);
        if (userExists is not null)
            return TypedResults.BadRequest(new ApiResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Errors = new List<string> { "User with this email already exists" }
            });

        var user = mapper.Map<ApplicationUser>(model);
        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return TypedResults.BadRequest(new ApiResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Errors = result.Errors.Select(e => e.Description).ToList()
            });

        var jwtToken = GenerateJwtToken(user, options.Value);

        return TypedResults.Ok(new ApiResponse<UserRegistrationVm>
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Data = new UserRegistrationVm { Token = jwtToken }
        });
    }

    private static async Task<Results<BadRequest<ApiResponse>, Ok<ApiResponse<LoginVm>>>> LoginUser(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IValidator<LoginUserDto> validator,
        IOptions<JwtConfig> options,
        [FromBody] LoginUserDto model)
    {
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
            return TypedResults.BadRequest(new ApiResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            });

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is null)
            return TypedResults.BadRequest(new ApiResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Errors = new List<string> { "User with this email does not exist" }
            });

        var isPasswordValid = await userManager.CheckPasswordAsync(user, model.Password);
        if (!isPasswordValid)
            return TypedResults.BadRequest(new ApiResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Errors = new List<string> { "Password is incorrect" }
            });

        var jwtToken = GenerateJwtToken(user, options.Value);

        return TypedResults.Ok(new ApiResponse<LoginVm>
        {
            IsSuccess = true,
            StatusCode = HttpStatusCode.OK,
            Data = new LoginVm { Token = jwtToken }
        });
    }

    private static string GenerateJwtToken(ApplicationUser user, JwtConfig jwtConfig)
    {
        var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString("en-GB"))
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        return jwtTokenHandler.WriteToken(token);
    }
}