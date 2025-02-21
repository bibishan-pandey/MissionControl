using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionControlSystem.Models;

public class Telemetry
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Set default value to current UTC time

    [ForeignKey("Spacecraft")] public int SpacecraftId { get; set; }

    public virtual Spacecraft? Spacecraft { get; set; }

    [ForeignKey("Satellite")] public int? SatelliteId { get; set; }

    public virtual Satellite? Satellite { get; set; }

    [ForeignKey("Mission")] public int MissionId { get; set; }

    public virtual Mission? Mission { get; set; }

    [Required] public TelemetryDataType TelemetryDataType { get; set; }

    [Required] [Range(0, 99999999.9999)] public decimal Value { get; set; }

    [Required] [StringLength(20)] public string Unit { get; set; }
}

// Enum for data types
public enum TelemetryDataType
{
    Temperature,
    Velocity,
    FuelLevel,
    Orientation,
    Pressure
}