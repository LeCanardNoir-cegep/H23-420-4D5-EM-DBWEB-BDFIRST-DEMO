using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sem09.Data;
using Sem09.Models;

namespace Sem09.Controllers
{
    public class SaisonsController : Controller
    {
        private readonly SeriesTVContext _context;

        public SaisonsController(SeriesTVContext context)
        {
            _context = context;
        }

        // GET: Saisons
        public async Task<IActionResult> Index()
        {
            var seriesTVContext = _context.Saisons.Include(s => s.Serie);
            return View(await seriesTVContext.ToListAsync());
        }

        // GET: Saisons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Saisons == null)
            {
                return NotFound();
            }

            var saison = await _context.Saisons
                .Include(s => s.Serie)
                .FirstOrDefaultAsync(m => m.SaisonId == id);
            if (saison == null)
            {
                return NotFound();
            }

            return View(saison);
        }

        // GET: Saisons/Create
        public IActionResult Create()
        {
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "SerieId");
            return View();
        }

        // POST: Saisons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaisonId,Num,NbEpisodes,SerieId")] Saison saison)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saison);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "SerieId", saison.SerieId);
            return View(saison);
        }

        // GET: Saisons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Saisons == null)
            {
                return NotFound();
            }

            var saison = await _context.Saisons.FindAsync(id);
            if (saison == null)
            {
                return NotFound();
            }
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "SerieId", saison.SerieId);
            return View(saison);
        }

        // POST: Saisons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaisonId,Num,NbEpisodes,SerieId")] Saison saison)
        {
            if (id != saison.SaisonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saison);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaisonExists(saison.SaisonId))
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
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "SerieId", saison.SerieId);
            return View(saison);
        }

        // GET: Saisons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Saisons == null)
            {
                return NotFound();
            }

            var saison = await _context.Saisons
                .Include(s => s.Serie)
                .FirstOrDefaultAsync(m => m.SaisonId == id);
            if (saison == null)
            {
                return NotFound();
            }

            return View(saison);
        }

        // POST: Saisons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Saisons == null)
            {
                return Problem("Entity set 'SeriesTVContext.Saisons'  is null.");
            }
            var saison = await _context.Saisons.FindAsync(id);
            if (saison != null)
            {
                _context.Saisons.Remove(saison);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaisonExists(int id)
        {
          return (_context.Saisons?.Any(e => e.SaisonId == id)).GetValueOrDefault();
        }
    }
}
