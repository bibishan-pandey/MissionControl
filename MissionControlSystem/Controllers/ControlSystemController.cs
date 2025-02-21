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
    public class ControlSystemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ControlSystemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ControlSystem
        public async Task<IActionResult> Index()
        {
            return View(await _context.ControlSystem.ToListAsync());
        }

        // GET: ControlSystem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlSystem = await _context.ControlSystem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (controlSystem == null)
            {
                return NotFound();
            }

            return View(controlSystem);
        }

        // GET: ControlSystem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControlSystem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,Version,Status")] ControlSystem controlSystem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(controlSystem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(controlSystem);
        }

        // GET: ControlSystem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlSystem = await _context.ControlSystem.FindAsync(id);
            if (controlSystem == null)
            {
                return NotFound();
            }
            return View(controlSystem);
        }

        // POST: ControlSystem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Version,Status")] ControlSystem controlSystem)
        {
            if (id != controlSystem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(controlSystem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControlSystemExists(controlSystem.Id))
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
            return View(controlSystem);
        }

        // GET: ControlSystem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlSystem = await _context.ControlSystem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (controlSystem == null)
            {
                return NotFound();
            }

            return View(controlSystem);
        }

        // POST: ControlSystem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var controlSystem = await _context.ControlSystem.FindAsync(id);
            if (controlSystem != null)
            {
                _context.ControlSystem.Remove(controlSystem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ControlSystemExists(int id)
        {
            return _context.ControlSystem.Any(e => e.Id == id);
        }
    }
}
