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
    public class ActeurSeriesController : Controller
    {
        private readonly SeriesTVContext _context;

        public ActeurSeriesController(SeriesTVContext context)
        {
            _context = context;
        }

        // GET: ActeurSeries
        public async Task<IActionResult> Index()
        {
            var seriesTVContext = _context.ActeurSeries.Include(a => a.Acteur).Include(a => a.Serie);
            return View(await seriesTVContext.ToListAsync());
        }

        // GET: ActeurSeries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ActeurSeries == null)
            {
                return NotFound();
            }

            var acteurSerie = await _context.ActeurSeries
                .Include(a => a.Acteur)
                .Include(a => a.Serie)
                .FirstOrDefaultAsync(m => m.ActeurSerieId == id);
            if (acteurSerie == null)
            {
                return NotFound();
            }

            return View(acteurSerie);
        }

        // GET: ActeurSeries/Create
        public IActionResult Create()
        {
            ViewData["ActeurId"] = new SelectList(_context.Acteurs, "ActeurId", "ActeurId");
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "SerieId");
            return View();
        }

        // POST: ActeurSeries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActeurSerieId,ActeurId,SerieId")] ActeurSerie acteurSerie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acteurSerie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActeurId"] = new SelectList(_context.Acteurs, "ActeurId", "ActeurId", acteurSerie.ActeurId);
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "SerieId", acteurSerie.SerieId);
            return View(acteurSerie);
        }

        // GET: ActeurSeries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ActeurSeries == null)
            {
                return NotFound();
            }

            var acteurSerie = await _context.ActeurSeries.FindAsync(id);
            if (acteurSerie == null)
            {
                return NotFound();
            }
            ViewData["ActeurId"] = new SelectList(_context.Acteurs, "ActeurId", "ActeurId", acteurSerie.ActeurId);
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "SerieId", acteurSerie.SerieId);
            return View(acteurSerie);
        }

        // POST: ActeurSeries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActeurSerieId,ActeurId,SerieId")] ActeurSerie acteurSerie)
        {
            if (id != acteurSerie.ActeurSerieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acteurSerie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActeurSerieExists(acteurSerie.ActeurSerieId))
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
            ViewData["ActeurId"] = new SelectList(_context.Acteurs, "ActeurId", "ActeurId", acteurSerie.ActeurId);
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "SerieId", acteurSerie.SerieId);
            return View(acteurSerie);
        }

        // GET: ActeurSeries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ActeurSeries == null)
            {
                return NotFound();
            }

            var acteurSerie = await _context.ActeurSeries
                .Include(a => a.Acteur)
                .Include(a => a.Serie)
                .FirstOrDefaultAsync(m => m.ActeurSerieId == id);
            if (acteurSerie == null)
            {
                return NotFound();
            }

            return View(acteurSerie);
        }

        // POST: ActeurSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ActeurSeries == null)
            {
                return Problem("Entity set 'SeriesTVContext.ActeurSeries'  is null.");
            }
            var acteurSerie = await _context.ActeurSeries.FindAsync(id);
            if (acteurSerie != null)
            {
                _context.ActeurSeries.Remove(acteurSerie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActeurSerieExists(int id)
        {
          return (_context.ActeurSeries?.Any(e => e.ActeurSerieId == id)).GetValueOrDefault();
        }
    }
}
