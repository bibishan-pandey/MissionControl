using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionControlSystem.Models;

public class Spacecraft
{
    [Key] public int Id { get; set; }

    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;

    [Required] [MaxLength(50)] public string Model { get; set; } = string.Empty;

    [Required] [MaxLength(100)] public string Manufacturer { get; set; } = string.Empty;

    public DateTime? LaunchDate { get; set; }

    // Foreign Key to Mission (nullable)
    public int? MissionId { get; set; }

    [ForeignKey("MissionId")] public Mission? Mission { get; set; }
}