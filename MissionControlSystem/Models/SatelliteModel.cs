using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionControlSystem.Models;

public class Satellite
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [StringLength(100)] public string Name { get; set; }

    [Required] public OrbitType OrbitType { get; set; }

    public DateTime? LaunchDate { get; set; }

    [Required] [StringLength(100)] public string Operator { get; set; }

    [Required] public SatelliteStatus Status { get; set; }

    [ForeignKey("Spacecraft")] public int? SpacecraftId { get; set; }

    public virtual Spacecraft? Spacecraft { get; set; }
}

// Enum for orbit types
public enum OrbitType
{
    LEO,
    MEO,
    GEO,
    HEO,
    Lagrange
}

// Enum for satellite statuses
public enum SatelliteStatus
{
    Active,
    Inactive,
    Decommissioned
}