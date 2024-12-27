using System.ComponentModel.DataAnnotations.Schema;
using TemplateProject.Enums;

namespace TemplateProject.Repositories.Models;

[Table("Locales")]
public class Locale
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public EntityStatus Status { get; set; }
    public int RegionId { get; set; }

    public virtual Region? Region { get; set; }
    public virtual IEnumerable<Dealership>? Dealerships { get; set; }
    public virtual IEnumerable<UserAccess>? UserAccess { get; set; }
}