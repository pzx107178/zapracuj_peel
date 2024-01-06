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
    public class BranzasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranzasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Branzas
        public async Task<IActionResult> Index()
        {
              return _context.Branza != null ? 
                          View(await _context.Branza.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Branza'  is null.");
        }

        // GET: Branzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Branza == null)
            {
                return NotFound();
            }

            var branza = await _context.Branza
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branza == null)
            {
                return NotFound();
            }

            return View(branza);
        }

        // GET: Branzas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Branzas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Branza branza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(branza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(branza);
        }

        // GET: Branzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Branza == null)
            {
                return NotFound();
            }

            var branza = await _context.Branza.FindAsync(id);
            if (branza == null)
            {
                return NotFound();
            }
            return View(branza);
        }

        // POST: Branzas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Branza branza)
        {
            if (id != branza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranzaExists(branza.Id))
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
            return View(branza);
        }

        // GET: Branzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Branza == null)
            {
                return NotFound();
            }

            var branza = await _context.Branza
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branza == null)
            {
                return NotFound();
            }

            return View(branza);
        }

        // POST: Branzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Branza == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Branza'  is null.");
            }
            var branza = await _context.Branza.FindAsync(id);
            if (branza != null)
            {
                _context.Branza.Remove(branza);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranzaExists(int id)
        {
          return (_context.Branza?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
