using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mapkowanie.Data;
using mapkowanie.Models;

namespace mapkowanie.Controllers
{
    public class RodzajesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RodzajesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rodzajes
        public async Task<IActionResult> Index()
        {
              return _context.Rodzaje != null ? 
                          View(await _context.Rodzaje.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Rodzaje'  is null.");
        }

        // GET: Rodzajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rodzaje == null)
            {
                return NotFound();
            }

            var rodzaje = await _context.Rodzaje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rodzaje == null)
            {
                return NotFound();
            }

            return View(rodzaje);
        }

        // GET: Rodzajes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rodzajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NazwaRoli")] Rodzaje rodzaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rodzaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rodzaje);
        }

        // GET: Rodzajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rodzaje == null)
            {
                return NotFound();
            }

            var rodzaje = await _context.Rodzaje.FindAsync(id);
            if (rodzaje == null)
            {
                return NotFound();
            }
            return View(rodzaje);
        }

        // POST: Rodzajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NazwaRoli")] Rodzaje rodzaje)
        {
            if (id != rodzaje.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rodzaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RodzajeExists(rodzaje.Id))
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
            return View(rodzaje);
        }

        // GET: Rodzajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rodzaje == null)
            {
                return NotFound();
            }

            var rodzaje = await _context.Rodzaje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rodzaje == null)
            {
                return NotFound();
            }

            return View(rodzaje);
        }

        // POST: Rodzajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rodzaje == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rodzaje'  is null.");
            }
            var rodzaje = await _context.Rodzaje.FindAsync(id);
            if (rodzaje != null)
            {
                _context.Rodzaje.Remove(rodzaje);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RodzajeExists(int id)
        {
          return (_context.Rodzaje?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
