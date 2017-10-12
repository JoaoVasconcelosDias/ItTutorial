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
    public class QuizPerguntasController : Controller
    {
        private readonly DataBaseContext _context;

        public QuizPerguntasController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: QuizPerguntas
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.QuizPergunta.Include(q => q.Quiz);
            return View(await dataBaseContext.ToListAsync());
        }
        
        // GET: QuizPerguntas/Details/5
        public async Task<IActionResult> Details(int? id)
            {                
            if (id == null)
            {
                return NotFound();
            }

            var quizPergunta = await _context.QuizPergunta
                .Include(q => q.Quiz)
                .SingleOrDefaultAsync(m => m.PerguntaId == id);
            if (quizPergunta == null)
            {
                return NotFound();
            }

            return View(quizPergunta);
        }

        // GET: QuizPerguntas/Create
        public IActionResult Create()
        {
            ViewData["QuizId"] = new SelectList(_context.Quiz, "QuizId", "QuizId");
            return View();
        }

        // POST: QuizPerguntas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuizId,PerguntaId,PerguntaExtenso,PerguntaOpcoes,RespostaCerta")] QuizPergunta quizPergunta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quizPergunta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuizId"] = new SelectList(_context.Quiz, "QuizId", "QuizId", quizPergunta.QuizId);
            return View(quizPergunta);
        }

        // GET: QuizPerguntas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizPergunta = await _context.QuizPergunta.SingleOrDefaultAsync(m => m.PerguntaId == id);
            if (quizPergunta == null)
            {
                return NotFound();
            }
            ViewData["QuizId"] = new SelectList(_context.Quiz, "QuizId", "QuizId", quizPergunta.QuizId);
            return View(quizPergunta);
        }

        // POST: QuizPerguntas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuizId,PerguntaId,PerguntaExtenso,PerguntaOpcoes,RespostaCerta")] QuizPergunta quizPergunta)
        {
            if (id != quizPergunta.PerguntaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizPergunta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizPerguntaExists(quizPergunta.PerguntaId))
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
            ViewData["QuizId"] = new SelectList(_context.Quiz, "QuizId", "QuizId", quizPergunta.QuizId);
            return View(quizPergunta);
        }

        // GET: QuizPerguntas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizPergunta = await _context.QuizPergunta
                .Include(q => q.Quiz)
                .SingleOrDefaultAsync(m => m.PerguntaId == id);
            if (quizPergunta == null)
            {
                return NotFound();
            }

            return View(quizPergunta);
        }

        // POST: QuizPerguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quizPergunta = await _context.QuizPergunta.SingleOrDefaultAsync(m => m.PerguntaId == id);
            _context.QuizPergunta.Remove(quizPergunta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizPerguntaExists(int id)
        {
            return _context.QuizPergunta.Any(e => e.PerguntaId == id);
        }
    }
}
