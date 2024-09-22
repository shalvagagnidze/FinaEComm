namespace ServiceLayer.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
    public string? Role {  get; set; }
}
