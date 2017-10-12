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
        
        public async Task<IActionResult>Index(int? Id)
        {
            
            var videos = _context.Videos.Where(m => m.LinguagemId == Id);

            if (Id == null)
            {
                return NotFound();
            }
            return View(videos);
        }


        // GET: Videos/Details/5
        public async Task<IActionResult> DetailsVideo(int? id)
        {
            if (id == null)
            {
                //return NotFound();
            }

            var video = await _context.Videos
                .SingleOrDefaultAsync(m => m.Id == id);
            if (video == null)
            {
                //return NotFound();
            }
            var videos = _context.Videos.Where(m => m.LinguagemId == video.LinguagemId);

            //var previousVideo = videos.Where(m => m.Id < video.Id).FirstOrDefault();
            //var nextVideo = videos.Where(m => m.Id > video.Id).FirstOrDefault();

            //List<Videos> videoList = new List<Videos>();
            ////videoList.Add(previousVideo);
            //videoList.Add(video);
            //videoList.Add(nextVideo);
            return View(video);
        }

        private bool VideosExists(int id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }

    }
    
}