using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MissionControlSystem.Data;
using MissionControlSystem.Models;

namespace MissionControlSystem.Controllers
{
    public class TelemetryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TelemetryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Telemetry
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Telemetry.Include(t => t.Mission).Include(t => t.Satellite).Include(t => t.Spacecraft);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Telemetry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telemetry = await _context.Telemetry
                .Include(t => t.Mission)
                .Include(t => t.Satellite)
                .Include(t => t.Spacecraft)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (telemetry == null)
            {
                return NotFound();
            }

            return View(telemetry);
        }

        // GET: Telemetry/Create
        public IActionResult Create()
        {
            ViewData["MissionId"] = new SelectList(_context.Mission, "Id", "Description");
            ViewData["SatelliteId"] = new SelectList(_context.Satellite, "Id", "Name");
            ViewData["SpacecraftId"] = new SelectList(_context.Spacecraft, "Id", "Manufacturer");
            return View();
        }

        // POST: Telemetry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Timestamp,SpacecraftId,SatelliteId,MissionId,TelemetryDataType,Value,Unit")] Telemetry telemetry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(telemetry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MissionId"] = new SelectList(_context.Mission, "Id", "Description", telemetry.MissionId);
            ViewData["SatelliteId"] = new SelectList(_context.Satellite, "Id", "Name", telemetry.SatelliteId);
            ViewData["SpacecraftId"] = new SelectList(_context.Spacecraft, "Id", "Manufacturer", telemetry.SpacecraftId);
            return View(telemetry);
        }

        // GET: Telemetry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telemetry = await _context.Telemetry.FindAsync(id);
            if (telemetry == null)
            {
                return NotFound();
            }
            ViewData["MissionId"] = new SelectList(_context.Mission, "Id", "Description", telemetry.MissionId);
            ViewData["SatelliteId"] = new SelectList(_context.Satellite, "Id", "Name", telemetry.SatelliteId);
            ViewData["SpacecraftId"] = new SelectList(_context.Spacecraft, "Id", "Manufacturer", telemetry.SpacecraftId);
            return View(telemetry);
        }

        // POST: Telemetry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Timestamp,SpacecraftId,SatelliteId,MissionId,TelemetryDataType,Value,Unit")] Telemetry telemetry)
        {
            if (id != telemetry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(telemetry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelemetryExists(telemetry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MissionId"] = new SelectList(_context.Mission, "Id", "Description", telemetry.MissionId);
            ViewData["SatelliteId"] = new SelectList(_context.Satellite, "Id", "Name", telemetry.SatelliteId);
            ViewData["SpacecraftId"] = new SelectList(_context.Spacecraft, "Id", "Manufacturer", telemetry.SpacecraftId);
            return View(telemetry);
        }

        // GET: Telemetry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telemetry = await _context.Telemetry
                .Include(t => t.Mission)
                .Include(t => t.Satellite)
                .Include(t => t.Spacecraft)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (telemetry == null)
            {
                return NotFound();
            }

            return View(telemetry);
        }

        // POST: Telemetry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var telemetry = await _context.Telemetry.FindAsync(id);
            if (telemetry != null)
            {
                _context.Telemetry.Remove(telemetry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TelemetryExists(int id)
        {
            return _context.Telemetry.Any(e => e.Id == id);
        }
    }
}
