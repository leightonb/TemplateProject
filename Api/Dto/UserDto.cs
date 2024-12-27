using TemplateProject.Repositories.Models;
using TemplateProject.Enums;

public class UserDto(int id, string username, string firstName, string lastName, string email, List<UserAccess>? userAccess)
{
    public int Id { get; set; } = id;
    public string Username { get; set; } = username;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
    public IEnumerable<UserAccessDto> UserAccess { get; set; } =
        userAccess?.Select(x => new UserAccessDto(x.Id, x.UserId, x.DealershipId, x.LocaleId, x.RegionId, x.BrandId, x.ManufacturerId, x.AccessLevel))
        ?? [];
}

public class UserAccessDto(int id, int userId, int? dealershipId, int? localeId, int? regionId, int? brandId, int? manufacturerId, AccessLevel accessLevel)
{
    public int Id { get; set; } = id;
    public int UserId { get; set; } = userId;
    public int? DealershipId { get; set; } = dealershipId;
    public int? LocaleId { get; set; } = localeId;
    public int? RegionId { get; set; } = regionId;
    public int? BrandId { get; set; } = brandId;
    public int? ManufacturerId { get; set; } = manufacturerId;
    public AccessLevel AccessLevel { get; set; } = accessLevel;
}