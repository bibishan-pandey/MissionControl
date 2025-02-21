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
    public class AsteroidController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsteroidController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Asteroid
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AsteroidModel.Include(a => a.Mission);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Asteroid/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asteroidModel = await _context.AsteroidModel
                .Include(a => a.Mission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asteroidModel == null)
            {
                return NotFound();
            }

            return View(asteroidModel);
        }

        // GET: Asteroid/Create
        public IActionResult Create()
        {
            ViewData["MissionId"] = new SelectList(_context.Mission, "Id", "Description");
            return View();
        }

        // POST: Asteroid/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DiameterKm,Composition,DistanceFromEarthAu,MissionId")] AsteroidModel asteroidModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asteroidModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MissionId"] = new SelectList(_context.Mission, "Id", "Description", asteroidModel.MissionId);
            return View(asteroidModel);
        }

        // GET: Asteroid/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asteroidModel = await _context.AsteroidModel.FindAsync(id);
            if (asteroidModel == null)
            {
                return NotFound();
            }
            ViewData["MissionId"] = new SelectList(_context.Mission, "Id", "Description", asteroidModel.MissionId);
            return View(asteroidModel);
        }

        // POST: Asteroid/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DiameterKm,Composition,DistanceFromEarthAu,MissionId")] AsteroidModel asteroidModel)
        {
            if (id != asteroidModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asteroidModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsteroidModelExists(asteroidModel.Id))
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
            ViewData["MissionId"] = new SelectList(_context.Mission, "Id", "Description", asteroidModel.MissionId);
            return View(asteroidModel);
        }

        // GET: Asteroid/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asteroidModel = await _context.AsteroidModel
                .Include(a => a.Mission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asteroidModel == null)
            {
                return NotFound();
            }

            return View(asteroidModel);
        }

        // POST: Asteroid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asteroidModel = await _context.AsteroidModel.FindAsync(id);
            if (asteroidModel != null)
            {
                _context.AsteroidModel.Remove(asteroidModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsteroidModelExists(int id)
        {
            return _context.AsteroidModel.Any(e => e.Id == id);
        }
    }
}
