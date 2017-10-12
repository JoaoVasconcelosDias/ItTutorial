using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItTutorial.Models;
using Microsoft.AspNetCore.Identity;

namespace ItTutorial.Controllers
{
    public class CommentsController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(DataBaseContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Comments.Include(c => c.AspNetUsers).Include(c => c.Posts);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments
                .Include(c => c.AspNetUsers)
                .Include(c => c.Posts)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["AspNetUsersId"] = new SelectList(_context.AspNetUsers, "Id", "Post");
            ViewData["PostsId"] = new SelectList(_context.Posts, "Id", "Post");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comment,Date,AspNetUsersId,PostsId")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                comments.AspNetUsersId = user.Id;

                DateTime localdate = DateTime.Now;
                comments.Date = localdate;

                _context.Add(comments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AspNetUsersId"] = new SelectList(_context.AspNetUsers, "Id", "Post", comments.AspNetUsersId);
            ViewData["PostsId"] = new SelectList(_context.Posts, "Id", "Post", comments.PostsId);
            return View(comments);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments.SingleOrDefaultAsync(m => m.Id == id);
            if (comments == null)
            {
                return NotFound();
            }
            ViewData["AspNetUsersId"] = new SelectList(_context.AspNetUsers, "Id", "Post", comments.AspNetUsersId);
            ViewData["PostsId"] = new SelectList(_context.Posts, "Id", "Post", comments.PostsId);
            return View(comments);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,Date,AspNetUsersId,PostsId")] Comments comments)
        {
            if (id != comments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    comments.AspNetUsersId = user.Id;

                    DateTime localdate = DateTime.Now;
                    comments.Date = localdate;

                    _context.Update(comments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentsExists(comments.Id))
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
            ViewData["AspNetUsersId"] = new SelectList(_context.AspNetUsers, "Id", "Post", comments.AspNetUsersId);
            ViewData["PostsId"] = new SelectList(_context.Posts, "Id", "AspNetUsersId", comments.PostsId);
            return View(comments);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments
                .Include(c => c.AspNetUsers)
                .Include(c => c.Posts)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comments = await _context.Comments.SingleOrDefaultAsync(m => m.Id == id);
            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentsExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
