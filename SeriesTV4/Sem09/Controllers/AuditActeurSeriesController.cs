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
    public class AuditActeurSeriesController : Controller
    {
        private readonly SeriesTVContext _context;

        public AuditActeurSeriesController(SeriesTVContext context)
        {
            _context = context;
        }

        // GET: AuditActeurSeries
        public async Task<IActionResult> Index()
        {
            var seriesTVContext = _context.AuditActeurSeries.Include(a => a.Acteur).Include(a => a.Serie);
            return View(await seriesTVContext.ToListAsync());
        }

        // GET: AuditActeurSeries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AuditActeurSeries == null)
            {
                return NotFound();
            }

            var auditActeurSerie = await _context.AuditActeurSeries
                .Include(a => a.Acteur)
                .Include(a => a.Serie)
                .FirstOrDefaultAsync(m => m.AuditActeurSerieId == id);
            if (auditActeurSerie == null)
            {
                return NotFound();
            }

            return View(auditActeurSerie);
        }

        // GET: AuditActeurSeries/Create
        public IActionResult Create()
        {
            ViewData["ActeurId"] = new SelectList(_context.Acteurs, "ActeurId", "ActeurId");
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "SerieId");
            return View();
        }

        // POST: AuditActeurSeries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuditActeurSerieId,Action,DateAction,ActeurId,SerieId")] AuditActeurSerie auditActeurSerie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auditActeurSerie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActeurId"] = new SelectList(_context.Acteurs, "ActeurId", "ActeurId", auditActeurSerie.ActeurId);
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "SerieId", auditActeurSerie.SerieId);
            return View(auditActeurSerie);
        }

        // GET: AuditActeurSeries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AuditActeurSeries == null)
            {
                return NotFound();
            }

            var auditActeurSerie = await _context.AuditActeurSeries.FindAsync(id);
            if (auditActeurSerie == null)
            {
                return NotFound();
            }
            ViewData["ActeurId"] = new SelectList(_context.Acteurs, "ActeurId", "ActeurId", auditActeurSerie.ActeurId);
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "SerieId", auditActeurSerie.SerieId);
            return View(auditActeurSerie);
        }

        // POST: AuditActeurSeries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuditActeurSerieId,Action,DateAction,ActeurId,SerieId")] AuditActeurSerie auditActeurSerie)
        {
            if (id != auditActeurSerie.AuditActeurSerieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auditActeurSerie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuditActeurSerieExists(auditActeurSerie.AuditActeurSerieId))
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
            ViewData["ActeurId"] = new SelectList(_context.Acteurs, "ActeurId", "ActeurId", auditActeurSerie.ActeurId);
            ViewData["SerieId"] = new SelectList(_context.Series, "SerieId", "SerieId", auditActeurSerie.SerieId);
            return View(auditActeurSerie);
        }

        // GET: AuditActeurSeries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AuditActeurSeries == null)
            {
                return NotFound();
            }

            var auditActeurSerie = await _context.AuditActeurSeries
                .Include(a => a.Acteur)
                .Include(a => a.Serie)
                .FirstOrDefaultAsync(m => m.AuditActeurSerieId == id);
            if (auditActeurSerie == null)
            {
                return NotFound();
            }

            return View(auditActeurSerie);
        }

        // POST: AuditActeurSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AuditActeurSeries == null)
            {
                return Problem("Entity set 'SeriesTVContext.AuditActeurSeries'  is null.");
            }
            var auditActeurSerie = await _context.AuditActeurSeries.FindAsync(id);
            if (auditActeurSerie != null)
            {
                _context.AuditActeurSeries.Remove(auditActeurSerie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuditActeurSerieExists(int id)
        {
          return (_context.AuditActeurSeries?.Any(e => e.AuditActeurSerieId == id)).GetValueOrDefault();
        }
    }
}
