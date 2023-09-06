using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusikPortal.Models;
using MusikPortal.Repository;
using System.IO;

namespace MusikPortal.Controllers
{
    public class AdminController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        IRepository rep;
        public AdminController(IRepository context, IWebHostEnvironment appEnvironment)
        {
            rep = context;
            _appEnvironment= appEnvironment;
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
        public async Task<IActionResult> EditArtist(int id)
        {
            Artist a = await rep.GetArtist(id);
            return View("EditArtist",a);
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
        public async Task<IActionResult> CreateArtist(Artist s, IFormFile p)
        {
            if (p != null)
            {
                string str = p.FileName.Replace(" ", "_");
                string str1 = str.Replace("-", "_");
                // Путь к папке Files
                string path = "/Photos/" + str1; // имя файла

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await p.CopyToAsync(fileStream); // копируем файл в поток
                }
                Artist art = new Artist();
                art.Name = s.Name;
                art.photo = path;
               
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
            ModelState.AddModelError("", "enter the photo");
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
                Artist a = await rep.GetArtist(s.Id);
                return View(a);
            }
            catch
            {
                await putArtists();
                return View("Artists");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteArtist(Artist s)
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
        public async Task<IActionResult> CancelDeleteArtist()
        {
            await putArtists();
            return View("Artists");
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
        public async Task<IActionResult> EditArtist(Artist s, IFormFile p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (p != null)
                    {
                        string str = p.FileName.Replace(" ", "_");
                        string str1 = str.Replace("-", "_");
                        // Путь к папке Files
                        string path = "/Photos/" +str1; // имя файла

                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await p.CopyToAsync(fileStream); // копируем файл в поток
                        }
                        await rep.EditArtist(s.Id, s.Name, path);
                    }
                    else
                        await rep.EditArtist(s.Id, s.Name,s.photo);
                    await rep.Save();
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    await putArtists();
                    return View("EditArtist");
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
                IEnumerable<User> s = await rep.GetUsers(HttpContext.Session.GetString("login"));
                ViewBag.Users = s;
                await rep.EditUser(u.Id, u.Level.Value);
                    await rep.Save();
                // return RedirectToAction("Index", "Home");
                return View("Users");
            }
                catch
                {
                IEnumerable<User> s = await rep.GetUsers(HttpContext.Session.GetString("login"));
                ViewBag.Users = s;
                await putUsers();
                    return View("Users");
                }
           
        }
        public async Task<IActionResult> DeleteSong(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Song s = await rep.GetSong(id);
            if (s == null)
            {
                return NotFound();
            }

            return View(s);
        }
        [HttpPost, ActionName("DeleteSong")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSongConfirmd(int id)
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
       [HttpPost, ActionName("CancelDeleteSong")]
        [ValidateAntiForgeryToken]
        public IActionResult CancelDeleteSong()
        {          
                return RedirectToAction("Index", "Home");
        }
        [HttpGet]
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
                int i = await rep.GetArtistId(s);
                int i1 = await rep.GetStyleId(s);
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
        public async Task<IActionResult> EditSong(AddSong s, IFormFile? p)
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
                        if (p != null)
                        {
                            // Путь к папке Files
                            string path = "/MusicFiles/" + p.FileName; // имя файла

                            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                            {
                                await p.CopyToAsync(fileStream); // копируем файл в поток
                            }
                            s.file = path;
                        }
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

