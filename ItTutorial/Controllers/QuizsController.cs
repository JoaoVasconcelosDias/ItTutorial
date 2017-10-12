using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItTutorial.Models;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Data;


namespace ItTutorial.Controllers
{
    public class QuizsController : Controller
    {
        private readonly DataBaseContext _context;
        private string DescricaoQuiz;
        private object btnSubmit1;

        public object RespostaCerta { get; private set; }

        public QuizsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Quizs
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Quiz.Include(q => q.LinguagemId);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Quizs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .Include(q => q.LinguagemId)
                .SingleOrDefaultAsync(m => m.QuizId == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Quizs/Create
        public IActionResult Create()
        {
            ViewData["Descricao"] = new SelectList(_context.Linguagem, "Descricao", "Descricao");
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuizId,LinguagemId")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quiz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Descricao"] = new SelectList(_context.Linguagem, "Descricao", "Descricao", quiz.Descricao);
            return View(quiz);
        }


        //public void Button_Click(string btnSubmit1, string btnSubmit2, string btnSubmit3, string btnSubmit4, object sender, System.EventArgs e)
        //{
        //    if (this.btnSubmit1.Checked)
        //    {
                
        //    }
        //    else if (btnSubmit2.Checked)
        //    {
                
        //    }
        //    else if (btnSubmit3.Checked)
        //    {
                
        //    }
        //    else (btnSubmit4.Checked)
        //    {

        //    }
        //}


       /* public void Button1_Click(string Radio1, string Radio2, string Radio3, string Radio4, object sender, System.EventArgs e)
        {
            if (Radio1.Checked == true)
            {

            }
            else if (Radio2.Checked == true)
            {

            }
            else if (Radio3.Checked)
            {

            }
            else (Radio4.Checked)
            {

            }
        }*/
    






        // GET: Quizs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz.SingleOrDefaultAsync(m => m.QuizId == id);
            if (quiz == null)
            {
                return NotFound();
            }
            ViewData["LinguagemId"] = new SelectList(_context.Linguagem, "LinguagemId", "LinguagemId", quiz.LinguagemId);
            return View(quiz);
        }

        // POST: Quizs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuizId,LinguagemId")] Quiz quiz)
        {
            if (id != quiz.QuizId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.QuizId))
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
            ViewData["LinguagemId"] = new SelectList(_context.Linguagem, "LinguagemId", "LinguagemId", quiz.LinguagemId);
            return View(quiz);
        }

        // GET: Quizs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .Include(q => q.LinguagemId)
                .SingleOrDefaultAsync(m => m.QuizId == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await _context.Quiz.SingleOrDefaultAsync(m => m.QuizId == id);
            _context.Quiz.Remove(quiz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(int id)
        {
            return _context.Quiz.Any(e => e.QuizId == id);
        }
    }
}
