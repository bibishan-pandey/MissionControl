using MissionControlSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace MissionControlSystem.Data
{
    public static class DbInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var context =
                new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // Ensure database is created
            context.Database.EnsureCreated();

            // Check if data already exists
            if (context.ControlSystem.Any() || context.Mission.Any() || context.Spacecraft.Any() ||
                context.Satellite.Any() || context.AsteroidModel.Any() || context.Telemetry.Any() ||
                context.Personnel.Any())
            {
                return; // DB has been seeded
            }

            // Control Systems
            var control1 = new ControlSystem
            {
                Id = 1, Name = "Mission Control Alpha", Location = "Houston, TX, USA", Version = "v3.2.1",
                Status = ControlSystemStatus.Online
            };
            var control2 = new ControlSystem
            {
                Id = 2, Name = "Orbital Operations Center", Location = "Darmstadt, Germany", Version = "v2.8.5",
                Status = ControlSystemStatus.Offline
            };
            var control3 = new ControlSystem
            {
                Id = 3, Name = "Deep Space Monitoring Hub", Location = "Canberra, Australia", Version = "v4.0.0",
                Status = ControlSystemStatus.UnderMaintenance
            };

            context.ControlSystem.AddRange(control1, control2, control3);
            context.SaveChanges();

            // Missions
            var mission1 = new Mission
            {
                Id = 1, Name = "Lunar Ice Survey", StartDate = new DateTime(2024, 3, 15),
                Status = MissionStatus.Ongoing, MissionType = MissionType.Exploration,
                Description = "Surveying ice deposits in the Moon’s south pole.", ControlSystem = control1
            };
            var mission2 = new Mission
            {
                Id = 2, Name = "Asteroid Resource Extraction A1", StartDate = new DateTime(2023, 11, 22),
                EndDate = new DateTime(2024, 6, 10), Status = MissionStatus.Completed, MissionType = MissionType.Mining,
                Description = "Extracted valuable minerals from asteroid 2023-KX.", ControlSystem = control2
            };
            var mission3 = new Mission
            {
                Id = 3, Name = "Europa Probe Deployment", StartDate = new DateTime(2025, 5, 20),
                Status = MissionStatus.Planned, MissionType = MissionType.Exploration,
                Description = "Deploying a lander to investigate subsurface oceans on Europa.", ControlSystem = control3
            };

            context.Mission.AddRange(mission1, mission2, mission3);
            context.SaveChanges();

            // Spacecraft
            var spacecraft1 = new Spacecraft
            {
                Id = 1, Name = "Orion-1", Model = "Orion MK-IV", Manufacturer = "Nova Aerospace",
                LaunchDate = new DateTime(2024, 3, 14), Mission = mission1
            };
            var spacecraft2 = new Spacecraft
            {
                Id = 2, Name = "AstroMiner-X", Model = "XR-9", Manufacturer = "Asteroid Industries",
                LaunchDate = new DateTime(2023, 11, 20), Mission = mission2
            };
            var spacecraft3 = new Spacecraft
            {
                Id = 3, Name = "Europa Explorer", Model = "EE-300", Manufacturer = "Deep Space Technologies",
                LaunchDate = new DateTime(2025, 5, 18), Mission = mission3
            };

            context.Spacecraft.AddRange(spacecraft1, spacecraft2, spacecraft3);
            context.SaveChanges();

            // Satellites
            context.Satellite.AddRange(
                new Satellite
                {
                    Id = 1, Name = "Lunar Relay-1", OrbitType = OrbitType.LEO, LaunchDate = new DateTime(2024, 3, 10),
                    Operator = "Nova Dynamics", Status = SatelliteStatus.Active, Spacecraft = spacecraft1
                },
                new Satellite
                {
                    Id = 2, Name = "Asteroid Scout-3", OrbitType = OrbitType.HEO,
                    LaunchDate = new DateTime(2023, 11, 15), Operator = "Asteroid Industries",
                    Status = SatelliteStatus.Decommissioned, Spacecraft = spacecraft2
                },
                new Satellite
                {
                    Id = 3, Name = "Europa Comms Orbiter", OrbitType = OrbitType.Lagrange,
                    LaunchDate = new DateTime(2025, 5, 10), Operator = "Deep Space Technologies",
                    Status = SatelliteStatus.Inactive, Spacecraft = spacecraft3
                }
            );
            context.SaveChanges();

            // Asteroids
            context.AsteroidModel.AddRange(
                new AsteroidModel
                {
                    Id = 1, Name = "2023-KX", DiameterKm = 1.75m, Composition = "Nickel-Iron, Silicates",
                    DistanceFromEarthAu = 2.1m, Mission = mission2
                },
                new AsteroidModel
                {
                    Id = 2, Name = "Apophis", DiameterKm = 0.37m, Composition = "Nickel-Iron",
                    DistanceFromEarthAu = 0.9m, Mission = null
                },
                new AsteroidModel
                {
                    Id = 3, Name = "16 Psyche", DiameterKm = 226m,
                    Composition = "Metal-rich (Iron, Nickel, Gold, Platinum)", DistanceFromEarthAu = 3.3m,
                    Mission = null
                }
            );
            context.SaveChanges();

            // Telemetry Data
            context.Telemetry.AddRange(
                new Telemetry
                {
                    Id = 1, Timestamp = new DateTime(2024, 3, 16, 14, 32, 0), Spacecraft = spacecraft1, SatelliteId = 1,
                    Mission = mission1, TelemetryDataType = TelemetryDataType.Temperature, Value = -75.2m,
                    Unit = "Celsius"
                },
                new Telemetry
                {
                    Id = 2, Timestamp = new DateTime(2024, 3, 16, 14, 35, 0), Spacecraft = spacecraft1, SatelliteId = 1,
                    Mission = mission1, TelemetryDataType = TelemetryDataType.Velocity, Value = 28000.5m, Unit = "km/h"
                },
                new Telemetry
                {
                    Id = 3, Timestamp = new DateTime(2023, 12, 1, 11, 20, 30), Spacecraft = spacecraft2,
                    SatelliteId = 2, Mission = mission2, TelemetryDataType = TelemetryDataType.FuelLevel, Value = 78.3m,
                    Unit = "Percent"
                }
            );
            context.SaveChanges();

            // Personnel
            context.Personnel.AddRange(
                new Personnel
                {
                    Id = 1, FirstName = "Dr. Emily", LastName = "Carter", PersonnelRole = PersonnelRole.Scientist,
                    Mission = mission1, ControlSystem = control1
                },
                new Personnel
                {
                    Id = 2, FirstName = "James", LastName = "Anderson", PersonnelRole = PersonnelRole.Engineer,
                    Mission = mission1, ControlSystem = control1
                },
                new Personnel
                {
                    Id = 3, FirstName = "Samantha", LastName = "Williams", PersonnelRole = PersonnelRole.MissionControl,
                    Mission = mission2, ControlSystem = control2
                },
                new Personnel
                {
                    Id = 4, FirstName = "Raj", LastName = "Patel", PersonnelRole = PersonnelRole.Pilot,
                    Mission = mission2, ControlSystem = control2
                },
                new Personnel
                {
                    Id = 5, FirstName = "Lisa", LastName = "Chen", PersonnelRole = PersonnelRole.Scientist,
                    Mission = mission3, ControlSystem = control3
                }
            );
            context.SaveChanges();

            Console.WriteLine("✅ Database seeding completed successfully!");
        }
    }
}