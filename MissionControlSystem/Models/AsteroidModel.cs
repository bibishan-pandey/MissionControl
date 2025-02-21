using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionControlSystem.Models;

public class AsteroidModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [StringLength(100)] public string Name { get; set; }

    [Required] [Range(0, 999999.99)] public decimal DiameterKm { get; set; }

    [Required] public string Composition { get; set; }

    [Required] [Range(0, 9999.9999)] public decimal DistanceFromEarthAu { get; set; }

    [ForeignKey("Mission")] public int? MissionId { get; set; }

    public virtual Mission? Mission { get; set; }
}