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
    public class PostsController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController(DataBaseContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Posts.Include(p => p.AspNetUsers).Include(p => p.Subcategorias);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.AspNetUsers)
                .Include(p => p.Subcategorias)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["AspNetUsersId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["SubcategoriasId"] = new SelectList(_context.Subcategorias, "Id", "Title");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,AspNetUsersId,SubcategoriasId,Post")] Posts posts, int? Id)
        {
            if (Id == null)
            {
                // se nao existir subcat
            }
            else
            {
                //se existir subcat
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                posts.AspNetUsersId = user.Id;


                DateTime localdate = DateTime.Now;
                posts.Date = localdate;

                _context.Add(posts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AspNetUsersId"] = new SelectList(_context.AspNetUsers, "Id", "Id", posts.AspNetUsersId);
            ViewData["SubcategoriasId"] = new SelectList(_context.Subcategorias, "Id", "Title", posts.SubcategoriasId);
            return View(posts);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (posts == null)
            {
                return NotFound();
            }
            ViewData["AspNetUsersId"] = new SelectList(_context.AspNetUsers, "Id", "Id", posts.AspNetUsersId);
            ViewData["SubcategoriasId"] = new SelectList(_context.Subcategorias, "Id", "Title", posts.SubcategoriasId);
            return View(posts);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,AspNetUsersId,SubcategoriasId,Post")] Posts posts)
        {
            if (id != posts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    posts.AspNetUsersId = user.Id;

                    DateTime localdate = DateTime.Now;
                    posts.Date = localdate;

                    _context.Update(posts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsExists(posts.Id))
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
            ViewData["AspNetUsersId"] = new SelectList(_context.AspNetUsers, "Id", "Id", posts.AspNetUsersId);
            ViewData["SubcategoriasId"] = new SelectList(_context.Subcategorias, "Id", "Title", posts.SubcategoriasId);
            return View(posts);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.AspNetUsers)
                .Include(p => p.Subcategorias)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posts = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            _context.Posts.Remove(posts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostsExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
