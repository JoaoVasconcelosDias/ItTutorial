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

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.Categorias.ToListAsync());
        }
    }
}