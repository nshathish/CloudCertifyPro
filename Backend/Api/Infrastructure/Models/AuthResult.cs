namespace Api.Infrastructure.Models;

public class AuthResult
{
    public string Token { get; set; } = null!;
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; } = null!;
}