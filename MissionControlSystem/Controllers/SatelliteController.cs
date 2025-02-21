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
    public class SatelliteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SatelliteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Satellite
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Satellite.Include(s => s.Spacecraft);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Satellite/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satellite = await _context.Satellite
                .Include(s => s.Spacecraft)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (satellite == null)
            {
                return NotFound();
            }

            return View(satellite);
        }

        // GET: Satellite/Create
        public IActionResult Create()
        {
            ViewData["SpacecraftId"] = new SelectList(_context.Spacecraft, "Id", "Manufacturer");
            return View();
        }

        // POST: Satellite/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,OrbitType,LaunchDate,Operator,Status,SpacecraftId")] Satellite satellite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(satellite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpacecraftId"] = new SelectList(_context.Spacecraft, "Id", "Manufacturer", satellite.SpacecraftId);
            return View(satellite);
        }

        // GET: Satellite/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satellite = await _context.Satellite.FindAsync(id);
            if (satellite == null)
            {
                return NotFound();
            }
            ViewData["SpacecraftId"] = new SelectList(_context.Spacecraft, "Id", "Manufacturer", satellite.SpacecraftId);
            return View(satellite);
        }

        // POST: Satellite/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,OrbitType,LaunchDate,Operator,Status,SpacecraftId")] Satellite satellite)
        {
            if (id != satellite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(satellite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SatelliteExists(satellite.Id))
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
            ViewData["SpacecraftId"] = new SelectList(_context.Spacecraft, "Id", "Manufacturer", satellite.SpacecraftId);
            return View(satellite);
        }

        // GET: Satellite/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satellite = await _context.Satellite
                .Include(s => s.Spacecraft)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (satellite == null)
            {
                return NotFound();
            }

            return View(satellite);
        }

        // POST: Satellite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var satellite = await _context.Satellite.FindAsync(id);
            if (satellite != null)
            {
                _context.Satellite.Remove(satellite);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SatelliteExists(int id)
        {
            return _context.Satellite.Any(e => e.Id == id);
        }
    }
}
