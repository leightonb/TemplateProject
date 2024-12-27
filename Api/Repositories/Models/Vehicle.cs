using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateProject.Repositories.Models;

[Table("Vehicles")]
public class Vehicle
{
    public int Id { get; set; }
    public string VIN { get; set; } = string.Empty;
    public int ManufacturerId { get; set; }
    public int BrandId { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }
    public virtual Brand? Brand { get; set; }
}