using Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace Data.Account;

public class ApplicationUser : IdentityUser<int>
{
    public TitleEnum? Title { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public DateTime? DateDeleted { get; set; }
}