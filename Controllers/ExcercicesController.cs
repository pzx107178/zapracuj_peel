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
using System.Diagnostics;

namespace mapkowanie.Controllers
{
    [Authorize]
    public class ExcercicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExcercicesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Staty
        public async Task<IActionResult> Staty()
        {
            IdentityUser uzytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;

            //Ponadto każdy użytkownik ma dostęp do swoich statystyk, które przy każdym rodzaju ćwiczenia podają liczbę sesji treningowych w ostatnich czterech tygodniach,
            //gdy użytkownik wykonywał dane ćwiczenie oraz najlepszy wynik(najlepszy to taki, gdy ciężar przemnożony przez łączną liczbę powtórzeń jest największy).

            DateTime czteryTygodnie = DateTime.Now.AddDays(4 * 7 * -1);
            var excercice = await _context.Excercice.Include(e => e.ExcerciceType).Include(e => e.Session).Include(e => e.user).Where(p => p.userId == uzytkownik.Id)
                                                    .Where(e => e.Session.Start >= czteryTygodnie).OrderByDescending(e => e.Weight).Select(e => e.Id).FirstOrDefaultAsync();

            var NajlepszyWynik = await _context.Excercice.Include(e => e.ExcerciceType).FirstOrDefaultAsync(m => m.Id == excercice);

            return View(NajlepszyWynik);
        }

        // GET: Excercices
        public async Task<IActionResult> Index()
        {
            IdentityUser uzytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;

            var applicationDbContext = _context.Excercice.Include(e => e.ExcerciceType).Include(e => e.Session);
            //return View(await applicationDbContext.Include(e => e.user).Where(p => p.userId == uzytkownik.Id).ToListAsync());
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Excercices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Excercice == null)
            {
                return NotFound();
            }

            var excercice = await _context.Excercice
                .Include(e => e.ExcerciceType)
                .Include(e => e.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (excercice == null)
            {
                return NotFound();
            }

            return View(excercice);
        }

        // GET: Excercices/Create
        public IActionResult Create()
        {
            IdentityUser uzytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;

            ViewData["ExcerciceTypeId"] = new SelectList(_context.ExcerciceType.Include(e => e.user).Where(p => p.userId == uzytkownik.Id), "Id", "Name");
            ViewData["SessionId"] = new SelectList(_context.Set<Session>().Include(e => e.user).Where(p => p.userId == uzytkownik.Id), "Id", "Start");
            return View();
        }

        // POST: Excercices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Weight,Reps,Series,ExcerciceTypeId,SessionId")] Excercice excercice)
        {
            IdentityUser uzytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;
            excercice.userId = uzytkownik.Id;

            if (ModelState.IsValid)
            {
                _context.Add(excercice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExcerciceTypeId"] = new SelectList(_context.ExcerciceType, "Id", "Name", excercice.ExcerciceTypeId);
            ViewData["SessionId"] = new SelectList(_context.Set<Session>(), "Id", "Start", excercice.SessionId);
            return View(excercice);
        }

        // GET: Excercices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Excercice == null)
            {
                return NotFound();
            }

            var excercice = await _context.Excercice.FindAsync(id);
            if (excercice == null)
            {
                return NotFound();
            }
            ViewData["ExcerciceTypeId"] = new SelectList(_context.ExcerciceType, "Id", "Name", excercice.ExcerciceTypeId);
            ViewData["SessionId"] = new SelectList(_context.Set<Session>(), "Id", "Start", excercice.SessionId);
            return View(excercice);
        }

        // POST: Excercices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Weight,Reps,Series,ExcerciceTypeId,SessionId")] Excercice excercice)
        {

            IdentityUser uzytkownik = _userManager.FindByNameAsync(User.Identity.Name).Result;
            excercice.userId = uzytkownik.Id;

            if (id != excercice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(excercice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExcerciceExists(excercice.Id))
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
            ViewData["ExcerciceTypeId"] = new SelectList(_context.ExcerciceType, "Id", "Name", excercice.ExcerciceTypeId);
            ViewData["SessionId"] = new SelectList(_context.Set<Session>(), "Id", "Start", excercice.SessionId);
            return View(excercice);
        }

        // GET: Excercices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Excercice == null)
            {
                return NotFound();
            }

            var excercice = await _context.Excercice
                .Include(e => e.ExcerciceType)
                .Include(e => e.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (excercice == null)
            {
                return NotFound();
            }

            return View(excercice);
        }

        // POST: Excercices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Excercice == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Excercice'  is null.");
            }
            var excercice = await _context.Excercice.FindAsync(id);
            if (excercice != null)
            {
                _context.Excercice.Remove(excercice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExcerciceExists(int id)
        {
          return (_context.Excercice?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
