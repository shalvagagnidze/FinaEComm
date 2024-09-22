using Microsoft.AspNetCore.Identity;

namespace DomainLayer.Entities;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
