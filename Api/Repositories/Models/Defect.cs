using System.ComponentModel.DataAnnotations.Schema;

namespace TemplateProject.Repositories.Models;

[Table("Defects")]
public class Defect
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int VehicleId { get; set; }
    public int DealershipId { get; set; }
}