using System.ComponentModel.DataAnnotations.Schema;
using TemplateProject.Enums;

namespace TemplateProject.Repositories.Models;

[Table("Users")]
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public byte[] Password { get; set; } = [];
    public byte[] PasswordKey { get; set; } = [];
    public byte[] PasswordIV { get; set; } = [];
    public string Role { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public EntityStatus Status { get; set; } = EntityStatus.New;
}