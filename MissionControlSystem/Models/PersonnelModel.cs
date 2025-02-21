using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionControlSystem.Models;

public class Personnel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [StringLength(50)] public string FirstName { get; set; }

    [Required] [StringLength(50)] public string LastName { get; set; }

    [Required] public PersonnelRole PersonnelRole { get; set; }

    [ForeignKey("Mission")] public int? MissionId { get; set; }

    public virtual Mission? Mission { get; set; }

    [ForeignKey("ControlSystem")] public int ControlSystemId { get; set; }

    public virtual ControlSystem? ControlSystem { get; set; }
}

// Enum for roles
public enum PersonnelRole
{
    Engineer,
    Scientist,
    MissionControl,
    Pilot
}