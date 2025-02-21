using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MissionControlSystem.Models;

namespace MissionControlSystem.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ControlSystem> ControlSystem { get; set; } = default!;

    public DbSet<MissionControlSystem.Models.Mission> Mission { get; set; } = default!;

    public DbSet<MissionControlSystem.Models.Spacecraft> Spacecraft { get; set; } = default!;

    public DbSet<MissionControlSystem.Models.Satellite> Satellite { get; set; } = default!;

    public DbSet<MissionControlSystem.Models.AsteroidModel> AsteroidModel { get; set; } = default!;

    public DbSet<MissionControlSystem.Models.Telemetry> Telemetry { get; set; } = default!;

    public DbSet<MissionControlSystem.Models.Personnel> Personnel { get; set; } = default!;
}