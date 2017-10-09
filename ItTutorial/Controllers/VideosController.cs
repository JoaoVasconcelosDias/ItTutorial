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
    public class VideosController : Controller
    {
        private readonly DataBaseContext _context;

        public VideosController(DataBaseContext context)
        {
            _context = context;
        }

        //02.10.2017
        // GET: Videos
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            //return View(await _context.Videos.ToListAsync());
            if (searchString !=null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var videos = from s in _context.Videos
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                videos = videos.Where(s => s.Title.Contains(searchString)
                                    || s.Source.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "LinguagemId":
                    videos = videos.OrderByDescending(s => s.Title);
                    break;

                case "Source":
                    videos = videos.OrderBy(s => s.LinguagemId);
                    break;
                default:
                    videos = videos.OrderBy(s => s.Title);
                    break;            
            }

            int pageSize = 3; //change the size of this page, in case there are more than 3 videos
            return View(await PaginatedList<Videos>.CreateAsync(videos.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videos = await _context.Videos
                .SingleOrDefaultAsync(m => m.Id == id);
            if (videos == null)
            {
                return NotFound();
            }

            return View(videos);
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Source,LinguagemId,Notes")] Videos videos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videos);
        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videos = await _context.Videos.SingleOrDefaultAsync(m => m.Id == id);
            if (videos == null)
            {
                return NotFound();
            }
            return View(videos);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Source,LinguagemId,Notes")] Videos videos)
        {
            if (id != videos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideosExists(videos.Id))
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
            return View(videos);
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videos = await _context.Videos
                .SingleOrDefaultAsync(m => m.Id == id);
            if (videos == null)
            {
                return NotFound();
            }

            return View(videos);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videos = await _context.Videos.SingleOrDefaultAsync(m => m.Id == id);
            _context.Videos.Remove(videos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideosExists(int id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }



        
    }

    
}
