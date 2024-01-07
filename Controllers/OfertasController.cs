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

    public class OfertasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OfertasController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Ofertas
        public async Task<IActionResult> Index()
        {
            IdentityUser uzytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;


                return _context.Oferta != null ? 
                          View(await _context.Oferta.Include(e => e.Konto).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Oferta'  is null.");
        }

        public async Task<IActionResult> Index2()
        {
            IdentityUser uzytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;


            return _context.Oferta != null ?
                      View(await _context.Oferta.Include(e => e.Konto).ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.Oferta'  is null.");
        }

        // GET: Ofertas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Oferta == null)
            {
                return NotFound();
            }

            var oferta = await _context.Oferta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oferta == null)
            {
                return NotFound();
            }

            return View(oferta);
        }

        // GET: Ofertas/Create
        public IActionResult Create()
        {
            IdentityUser uzytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;

            return View();
        }

        // POST: Ofertas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Opis,WynagrodzenieMin,WynagrodzenieMax,PracaStart,PracaStop,widocznosc")] Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oferta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oferta);
        }

        // GET: Ofertas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            IdentityUser uzytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;

            if (id == null || _context.Oferta == null)
            {
                return NotFound();
            }

            var oferta = await _context.Oferta.FindAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }
            return View(oferta);
        }

        // POST: Ofertas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Opis,WynagrodzenieMin,WynagrodzenieMax,PracaStart,PracaStop,widocznosc")] Oferta oferta)
        {
            if (id != oferta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oferta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfertaExists(oferta.Id))
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
            return View(oferta);
        }

        // GET: Ofertas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Oferta == null)
            {
                return NotFound();
            }

            var oferta = await _context.Oferta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oferta == null)
            {
                return NotFound();
            }

            return View(oferta);
        }

        // POST: Ofertas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Oferta == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Oferta'  is null.");
            }
            var oferta = await _context.Oferta.FindAsync(id);
            if (oferta != null)
            {
                _context.Oferta.Remove(oferta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfertaExists(int id)
        {
          return (_context.Oferta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
