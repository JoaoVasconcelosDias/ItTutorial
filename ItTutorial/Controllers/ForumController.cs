using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItTutorial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ItTutorial.Controllers
{
    public class ForumController : Controller
    {
        private readonly DataBaseContext _context;

        public ForumController(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Categorias.Include(c => c.Subcategorias);
            return View(await dataBaseContext.ToListAsync());
        }

        [Route("Forum/Subcategoria/{nomeSub}/{postId?}")]
        public ActionResult Subcategoria(string nomeSub, int? postId)
        {
            if( postId == null)
            {
                //se estivermos na subcategoria
                
                var result = _context.Subcategorias.Include(p => p.Posts).SingleOrDefault(p => p.Title == nomeSub);
                if(result == null)
                {
                    return RedirectToAction("Index");
                }
                return View(result);

            }


            else
            {
                //se estivermos num post
                var result = _context.Posts.Include(p => p.Comments).SingleOrDefault(p => p.Id == postId);
                if (result == null)
                {
                    return RedirectToAction("Index");
                }
                return View("PostView", result);
            }
        }
    }
}