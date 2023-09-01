using Microsoft.AspNetCore.Mvc;
using MusikPortal.Models;
using MusikPortal.Repository;
using System.Diagnostics;

namespace MusikPortal.Controllers
{
    public class HomeController : Controller
    {
        IRepository rep;
        public HomeController(IRepository context)
        {
            rep = context;          
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Song> s = await rep.GetSongs();
            ViewBag.Songs = s;
            return View();
        }
        public async Task<IActionResult> Find(string str)
        {
            IEnumerable<Song> s = await rep.FindSongs(str);
            ViewBag.Songs = s;
            return View("Index");
        }
    }
}