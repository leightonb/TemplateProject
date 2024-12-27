using System.ComponentModel.DataAnnotations.Schema;
using TemplateProject.Enums;

namespace TemplateProject.Repositories.Models;

[Table("Regions")]
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public EntityStatus Status { get; set; }
    public int BrandId { get; set; }

    public virtual Brand? Brand { get; set; }
    public virtual IEnumerable<Locale>? Locales { get; set; }
    public virtual IEnumerable<UserAccess>? UserAccess { get; set; }
}