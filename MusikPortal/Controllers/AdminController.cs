using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusikPortal.Models;
using MusikPortal.Repository;

namespace MusikPortal.Controllers
{
    public class AdminController : Controller
    {
        IRepository rep;
        public AdminController(IRepository context)
        {
            rep = context;
        }
        public async Task<IActionResult> Styles()
        {
            List<Style> s = await rep.GetStylesList();
            ViewData["StyleId"] = new SelectList(s, "Id", "Name");
            return View("Styles");
        }
        public async Task<IActionResult> Artists()
        {
            List<Artist> a = await rep.GetArtistsList();
            ViewData["ArtistId"] = new SelectList(a, "Id", "Name");
            return View("Artists");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStyle(Style s)
        {
            Style style = new Style();
            style.Name = s.Name;
            if (ModelState.IsValid)
            {
                try
                {
                    await rep.AddStyle(style);
                    await rep.Save();
                    return RedirectToAction("Index", "Home");                   
                }
                catch
                {
                    putStyles(); 
                    return View("Styles", s);
                }

            }
            putStyles();
            return View("Styles", s);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArtist(Artist s)
        {
           Artist art = new Artist();
            art.Name = s.Name;
            if (ModelState.IsValid)
            {
                try
                {
                    await rep.AddArtist(art);
                    await rep.Save();
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    putArtists();
                    return View("Artists", s);
                }

            }
            putArtists();
            return View("Artists", s);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStyle(Style s)
        {                  
                try
                {
                    await rep.DeleteStyle(s.Id);
                    await rep.Save();
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                putStyles(); 
                return View("Styles");
                }           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteArtist(Artist s)
        {
            try
            {
                await rep.DeleteArtist(s.Id);
                await rep.Save();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                putArtists();
                return View("Artists");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStyle(Style s)
        {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await rep.EditStyle(s.Id,s.Name);
                        await rep.Save();
                        return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        putStyles();
                        return View("Styles");
                    }

                }
            putStyles();
            return View("Styles");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArtist(Artist s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await rep.EditArtist(s.Id, s.Name);
                    await rep.Save();
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    putArtists();
                    return View("Artists");
                }

            }
            putArtists();
            return View("Artists");
        }
        public async void putStyles()
        {
            List<Style> s = await rep.GetStylesList();
            ViewData["StyleId"] = new SelectList(s, "Id", "Name");          
        }
        public async void putArtists()
        {
            List<Artist> a = await rep.GetArtistsList();
            ViewData["ArtistId"] = new SelectList(a, "Id", "Name");
        }
        public async void putUsers()
        {
            IEnumerable<User> s = await rep.GetUsers(HttpContext.Session.GetString("login"));
            ViewBag.Users = s;
        }
        public async Task<IActionResult> Users()
        {
            IEnumerable<User> s = await rep.GetUsers(HttpContext.Session.GetString("login"));
            ViewBag.Users = s;
            return View();
        }
        public async Task<IActionResult> EditUser(User u)
        {            
                try
                {
                    await rep.EditUser(u.Id, u.Level.Value);
                    await rep.Save();
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    putUsers();
                    return View("Users");
                }
           
        }      
        public async Task<IActionResult> DeleteSong(int id)
        {
            try
            {
                await rep.DeleteSong(id);
                await rep.Save();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Index", "Home");
            }
        }
    }
}
