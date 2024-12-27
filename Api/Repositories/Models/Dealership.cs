using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateProject.Repositories.Models;

[Table("Dealerships")]
public class Dealership
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int LocaleId { get; set; }

    public virtual Locale? Locale { get; set; }
    public virtual IEnumerable<UserAccess>? UserAccess { get; set; }
}