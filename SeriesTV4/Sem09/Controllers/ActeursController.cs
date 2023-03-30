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
    public class ActeursController : Controller
    {
        private readonly SeriesTVContext _context;

        public ActeursController(SeriesTVContext context)
        {
            _context = context;
        }

        // GET: Acteurs
        public async Task<IActionResult> Index()
        {
              return _context.Acteurs != null ? 
                          View(await _context.Acteurs.ToListAsync()) :
                          Problem("Entity set 'SeriesTVContext.Acteurs'  is null.");
        }

        // GET: Acteurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Acteurs == null)
            {
                return NotFound();
            }

            var acteur = await _context.Acteurs
                .FirstOrDefaultAsync(m => m.ActeurId == id);
            if (acteur == null)
            {
                return NotFound();
            }

            return View(acteur);
        }

        // GET: Acteurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Acteurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActeurId,Prenom,Nom,DateNaissance,DateDeces")] Acteur acteur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acteur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(acteur);
        }

        // GET: Acteurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Acteurs == null)
            {
                return NotFound();
            }

            var acteur = await _context.Acteurs.FindAsync(id);
            if (acteur == null)
            {
                return NotFound();
            }
            return View(acteur);
        }

        // POST: Acteurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActeurId,Prenom,Nom,DateNaissance,DateDeces")] Acteur acteur)
        {
            if (id != acteur.ActeurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acteur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActeurExists(acteur.ActeurId))
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
            return View(acteur);
        }

        // GET: Acteurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Acteurs == null)
            {
                return NotFound();
            }

            var acteur = await _context.Acteurs
                .FirstOrDefaultAsync(m => m.ActeurId == id);
            if (acteur == null)
            {
                return NotFound();
            }

            return View(acteur);
        }

        // POST: Acteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Acteurs == null)
            {
                return Problem("Entity set 'SeriesTVContext.Acteurs'  is null.");
            }
            var acteur = await _context.Acteurs.FindAsync(id);
            if (acteur != null)
            {
                _context.Acteurs.Remove(acteur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActeurExists(int id)
        {
          return (_context.Acteurs?.Any(e => e.ActeurId == id)).GetValueOrDefault();
        }
    }
}
