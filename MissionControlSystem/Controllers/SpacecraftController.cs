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
    public class SpacecraftController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpacecraftController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Spacecraft
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Spacecraft.Include(s => s.Mission);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Spacecraft/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spacecraft = await _context.Spacecraft
                .Include(s => s.Mission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spacecraft == null)
            {
                return NotFound();
            }

            return View(spacecraft);
        }

        // GET: Spacecraft/Create
        public IActionResult Create()
        {
            ViewData["MissionId"] = new SelectList(_context.Mission, "Id", "Description");
            return View();
        }

        // POST: Spacecraft/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Model,Manufacturer,LaunchDate,MissionId")] Spacecraft spacecraft)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spacecraft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MissionId"] = new SelectList(_context.Mission, "Id", "Description", spacecraft.MissionId);
            return View(spacecraft);
        }

        // GET: Spacecraft/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spacecraft = await _context.Spacecraft.FindAsync(id);
            if (spacecraft == null)
            {
                return NotFound();
            }
            ViewData["MissionId"] = new SelectList(_context.Mission, "Id", "Description", spacecraft.MissionId);
            return View(spacecraft);
        }

        // POST: Spacecraft/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,Manufacturer,LaunchDate,MissionId")] Spacecraft spacecraft)
        {
            if (id != spacecraft.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spacecraft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpacecraftExists(spacecraft.Id))
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
            ViewData["MissionId"] = new SelectList(_context.Mission, "Id", "Description", spacecraft.MissionId);
            return View(spacecraft);
        }

        // GET: Spacecraft/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spacecraft = await _context.Spacecraft
                .Include(s => s.Mission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spacecraft == null)
            {
                return NotFound();
            }

            return View(spacecraft);
        }

        // POST: Spacecraft/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spacecraft = await _context.Spacecraft.FindAsync(id);
            if (spacecraft != null)
            {
                _context.Spacecraft.Remove(spacecraft);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpacecraftExists(int id)
        {
            return _context.Spacecraft.Any(e => e.Id == id);
        }
    }
}
