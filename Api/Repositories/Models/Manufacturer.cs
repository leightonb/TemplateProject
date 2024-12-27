using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateProject.Repositories.Models;

[Table("Manufacturers")]
public class Manufacturer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;

    public virtual IEnumerable<Brand>? Brands { get; set; }
    public virtual IEnumerable<Vehicle>? Vehicles { get; set; }
    public virtual IEnumerable<UserAccess>? UserAccess { get; set; }
}