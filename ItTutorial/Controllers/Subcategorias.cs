using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItTutorial.Models;

namespace ItTutorial.Controllers
{
    public class SubcategoriasController : Controller
    {
        private readonly DataBaseContext _context;

        public SubcategoriasController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Subcategorias
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Subcategorias.Include(s => s.Categorias);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Subcategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategorias = await _context.Subcategorias
                .Include(s => s.Categorias)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (subcategorias == null)
            {
                return NotFound();
            }

            return View(subcategorias);
        }

        // GET: Subcategorias/Create
        public IActionResult Create()
        {
            ViewData["CategoriasId"] = new SelectList(_context.Categorias, "Id", "CategoryTitle");
            return View();
        }

        // POST: Subcategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoriasId,Title")] Subcategorias subcategorias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subcategorias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriasId"] = new SelectList(_context.Categorias, "Id", "CategoryTitle", subcategorias.CategoriasId);
            return View(subcategorias);
        }

        // GET: Subcategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategorias = await _context.Subcategorias.SingleOrDefaultAsync(m => m.Id == id);
            if (subcategorias == null)
            {
                return NotFound();
            }
            ViewData["CategoriasId"] = new SelectList(_context.Categorias, "Id", "CategoryTitle", subcategorias.CategoriasId);
            return View(subcategorias);
        }

        // POST: Subcategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoriasId,Title")] Subcategorias subcategorias)
        {
            if (id != subcategorias.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subcategorias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubcategoriasExists(subcategorias.Id))
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
            ViewData["CategoriasId"] = new SelectList(_context.Categorias, "Id", "CategoryTitle", subcategorias.CategoriasId);
            return View(subcategorias);
        }

        // GET: Subcategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategorias = await _context.Subcategorias
                .Include(s => s.Categorias)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (subcategorias == null)
            {
                return NotFound();
            }

            return View(subcategorias);
        }

        // POST: Subcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subcategorias = await _context.Subcategorias.SingleOrDefaultAsync(m => m.Id == id);
            _context.Subcategorias.Remove(subcategorias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubcategoriasExists(int id)
        {
            return _context.Subcategorias.Any(e => e.Id == id);
        }
    }
}
