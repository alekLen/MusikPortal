using Microsoft.AspNetCore.Mvc;
using MusikPortal.Models;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using System.Diagnostics;

namespace MusikPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISongService songService;
        public HomeController(  ISongService song)
        {
            songService = song;          
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<SongDTO> s = await songService.GetAllSongs();
            ViewBag.Songs = s;
            return View();
        }
        public async Task<IActionResult> Find(string str)
        {
            IEnumerable<SongDTO> s = await songService.FindSongs(str);
            ViewBag.Songs = s;
            return View("Index");
        }
    }
}