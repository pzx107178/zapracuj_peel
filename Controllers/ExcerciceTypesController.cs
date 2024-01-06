using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mapkowanie.Data;
using mapkowanie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace mapkowanie.Controllers
{
    [Authorize]
    public class ExcerciceTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExcerciceTypesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ExcerciceTypes
        public async Task<IActionResult> Index()
        {
            IdentityUser uzytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;

            return _context.ExcerciceType != null ? 
                          View(await _context.ExcerciceType.Include(e => e.user).Where(p => p.userId == uzytkownik.Id).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ExcerciceType'  is null.");
        }

        // GET: ExcerciceTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExcerciceType == null)
            {
                return NotFound();
            }

            var excerciceType = await _context.ExcerciceType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (excerciceType == null)
            {
                return NotFound();
            }

            return View(excerciceType);
        }

        // GET: ExcerciceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExcerciceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ExcerciceType excerciceType)
        {
            IdentityUser uzytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;
            excerciceType.userId = uzytkownik.Id;

            if (ModelState.IsValid)
            {
                _context.Add(excerciceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(excerciceType);
        }

        // GET: ExcerciceTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            

            if (id == null || _context.ExcerciceType == null)
            {
                return NotFound();
            }

            var excerciceType = await _context.ExcerciceType.FindAsync(id);
            if (excerciceType == null)
            {
                return NotFound();
            }
            return View(excerciceType);
        }

        // POST: ExcerciceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ExcerciceType excerciceType)
        {
            IdentityUser uzytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;
            excerciceType.userId = uzytkownik.Id;

            if (id != excerciceType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(excerciceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExcerciceTypeExists(excerciceType.Id))
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
            return View(excerciceType);
        }

        // GET: ExcerciceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExcerciceType == null)
            {
                return NotFound();
            }

            var excerciceType = await _context.ExcerciceType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (excerciceType == null)
            {
                return NotFound();
            }

            return View(excerciceType);
        }

        // POST: ExcerciceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExcerciceType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ExcerciceType'  is null.");
            }
            var excerciceType = await _context.ExcerciceType.FindAsync(id);
            if (excerciceType != null)
            {
                _context.ExcerciceType.Remove(excerciceType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExcerciceTypeExists(int id)
        {
          return (_context.ExcerciceType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
