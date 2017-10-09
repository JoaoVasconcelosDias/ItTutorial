using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItTutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace ItTutorial.Controllers
{
    public class ForumController : Controller
    {
        private readonly DataBaseContext _context;

        public ForumController(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id, [Bind("Id,Title")] Subcategorias subcategorias)
        {
            var dataBaseContext = _context.Categorias.Include(c => c.Subcategorias);
            return View(await dataBaseContext.ToListAsync());
        }

        public async Task<IActionResult> PostsIndex()
        {
            var dataBaseContext = _context.Subcategorias.Include(c => c.Posts);
            return View(await dataBaseContext.ToListAsync());
        }

    }
}