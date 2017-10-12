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
    public class LinguagemsController : Controller
    {
        private readonly DataBaseContext _context;

        public LinguagemsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Linguagems
        public async Task<IActionResult> Index()
        {
            return View(await _context.Linguagem.ToListAsync());
        }

        // GET: Linguagems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linguagem = await _context.Linguagem
                .SingleOrDefaultAsync(m => m.LinguagemId == id);
            if (linguagem == null)
            {
                return NotFound();
            }

            return View(linguagem);
        }

        // GET: Linguagems/Create
        public IActionResult Create()
        {
            ViewData["LinguagemId"] = new SelectList(_context.Linguagem, "LinguagemId", "LinguagemDescricao");
            return View();
        }

        // POST: Linguagems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LinguagemId")] Linguagem linguagem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(linguagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(linguagem);
        }

        // GET: Linguagems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linguagem = await _context.Linguagem.SingleOrDefaultAsync(m => m.LinguagemId == id);
            if (linguagem == null)
            {
                return NotFound();
            }
            return View(linguagem);
        }

        // POST: Linguagems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LinguagemId")] Linguagem linguagem)
        {
            if (id != linguagem.LinguagemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(linguagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinguagemExists(linguagem.LinguagemId))
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
            return View(linguagem);
        }

        // GET: Linguagems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linguagem = await _context.Linguagem
                .SingleOrDefaultAsync(m => m.LinguagemId == id);
            if (linguagem == null)
            {
                return NotFound();
            }

            return View(linguagem);
        }

        // POST: Linguagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var linguagem = await _context.Linguagem.SingleOrDefaultAsync(m => m.LinguagemId == id);
            _context.Linguagem.Remove(linguagem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinguagemExists(int id)
        {
            return _context.Linguagem.Any(e => e.LinguagemId == id);
        }
    }
}
