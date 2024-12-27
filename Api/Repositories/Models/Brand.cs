using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateProject.Repositories.Models;

[Table("Brands")]
public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ManufacturerId { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }
    public virtual IEnumerable<Region>? Regions { get; set; }
    public virtual IEnumerable<UserAccess>? UserAccess { get; set; }
}