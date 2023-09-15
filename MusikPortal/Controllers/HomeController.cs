using Microsoft.AspNetCore.Mvc;
using MusikPortal.Models;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using System.Diagnostics;
using AutoMapper;
using Newtonsoft.Json;

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
        [HttpPost]
        public async Task<IActionResult> Find(string str)
        {
            IEnumerable<SongDTO> s = await songService.FindSongs(str);
            string response = JsonConvert.SerializeObject(s);
            return Json(response);
        }
        public async Task<IActionResult> AllSongs()
        {
            IEnumerable<SongDTO> s = await songService.GetAllSongs();
            string response = JsonConvert.SerializeObject(s);
             return Json(response);
           
        }
    }
}