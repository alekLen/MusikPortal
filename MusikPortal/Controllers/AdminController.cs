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
                    await putStyles(); 
                    return View("Styles", s);
                }

            }
            await putStyles();
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
                    await putArtists();
                    return View("Artists", s);
                }

            }
            await putArtists();
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
                await putStyles(); 
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
                await putArtists();
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
                        await putStyles();
                        return View("Styles");
                    }

                }
            await putStyles();
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
                    await putArtists();
                    return View("Artists");
                }

            }
            await putArtists();
            return View("Artists");
        }
        public async Task putStyles()
        {
            List<Style> s = await rep.GetStylesList();
            ViewData["StyleId"] = new SelectList(s, "Id", "Name");          
        }
        public async Task putArtists()
        {
            List<Artist> a = await rep.GetArtistsList();
            ViewData["ArtistId"] = new SelectList(a, "Id", "Name");
        }
        public async Task putUsers()
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
                   await putUsers();
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
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> EditSong(int id)
        {
            try
            {
                Song s=await rep.GetSong(id);
                AddSong s1 = new();
                s1.SongId = id;
                s1.Name= s.Name;
                s1.Year= s.Year;
                s1.Album= s.Album;
                s1.text = s.text;
                int i = await rep.GetArtistId(id);
                int i1 = await rep.GetStyleId(id);
                s1.ArtistId = i;
                s1.StyleId = i1;
                s1.file = s.file;
                await putStyles();
                await putArtists();
                return View("EditSong",s1);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSong(AddSong s)
        {
            try
            {
                DateTime today = DateTime.Today;
                int currentYear = today.Year;
                try
                {
                    if (Convert.ToInt32(s.Year) < 0 || Convert.ToInt32(s.Year) > currentYear)
                        ModelState.AddModelError("", "uncorrectly year");
                }
                catch { ModelState.AddModelError("", "uncorrectly year"); }
                if (ModelState.IsValid)
                {
                    try
                    {
                        Song song = await rep.GetSong(s.SongId.Value);
                    song.Name = s.Name;
                    song.Year = s.Year;
                    song.Album = s.Album;
                    song.artist = await rep.GetArtist(s.ArtistId);
                    song.style = await rep.GetStyle(s.StyleId);
                    song.file = s.file;
                    song.text = s.text;
                    await rep.UpdateSong(song);
                    await rep.Save();
                    return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        await putStyles();
                        await putArtists();
                        return View("EditSong", s);
                    }
                }
                else
                {
                    await putStyles();
                    await putArtists();
                    return View("EditSong", s);
                }
            }
            catch
            {
                await putStyles();
                await putArtists();
                return View("EditSong", s);
            }
        }
    }
}

