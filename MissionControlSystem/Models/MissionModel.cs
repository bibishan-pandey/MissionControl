using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionControlSystem.Models;

public enum MissionStatus
{
    Planned,
    Ongoing,
    Completed,
    Aborted
}

public enum MissionType
{
    Exploration,
    Mining,
    Research
}

public class Mission
{
    [Key] public int Id { get; set; }

    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;

    [Required] public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required] public MissionStatus Status { get; set; }

    [Required] public MissionType MissionType { get; set; }

    [Required] public string Description { get; set; } = string.Empty;

    // Foreign Key to ControlSystem
    [Required] public int ControlSystemId { get; set; }

    [ForeignKey("ControlSystemId")] public ControlSystem? ControlSystem { get; set; } = null!;
}