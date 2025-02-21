using System.ComponentModel.DataAnnotations;

namespace MissionControlSystem.Models;

public enum ControlSystemStatus
{
    Online,
    Offline,
    UnderMaintenance
}

public class ControlSystem
{
    [Key] public int Id { get; set; }

    [Required] [MaxLength(100)] public string Name { get; set; }

    [Required] [MaxLength(255)] public string Location { get; set; }

    [Required] [MaxLength(20)] public string Version { get; set; }

    [Required]
    [EnumDataType(typeof(ControlSystemStatus))]
    public ControlSystemStatus Status { get; set; }
}