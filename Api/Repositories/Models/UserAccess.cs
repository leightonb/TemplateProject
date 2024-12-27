using System.ComponentModel.DataAnnotations.Schema;
using TemplateProject.Enums;

namespace TemplateProject.Repositories.Models;

[Table("UserAccess")]
public class UserAccess
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? DealershipId { get; set; }
    public int? LocaleId { get; set; }
    public int? RegionId { get; set; }
    public int? BrandId { get; set; }
    public int? ManufacturerId { get; set; }
    public AccessLevel AccessLevel { get; set; } = AccessLevel.None;

    public virtual required User User { get; set; }
    public virtual Dealership? Dealership { get; set; }
    public virtual Locale? Locale { get; set; }
    public virtual Region? Region { get; set; }
    public virtual Brand? Brand { get; set; }
    public virtual Manufacturer? Manufacturer { get; set; }
}