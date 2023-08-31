using Microsoft.AspNetCore.Mvc;
using MusikPortal.Repository;
using MusikPortal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace MusikPortal.Controllers
{
    public class SongController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        IRepository rep;
        public SongController(IRepository context,IWebHostEnvironment appEnvironment)
        {
            rep = context;
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> Create()
        {
            await putStylesArtists();
            return View("AddSong");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( AddSong song, IFormFile uploadedFile)
        {            
            if (uploadedFile == null)
                ModelState.AddModelError("", "put the file");
            DateTime today = DateTime.Today;
            int currentYear = today.Year;
            try
            {
                if (Convert.ToInt32(song.Year) < 0 || Convert.ToInt32(song.Year) > currentYear)
                    ModelState.AddModelError("", "uncorrectly year");
            }
            catch { ModelState.AddModelError("", "uncorrectly year"); }
            if (uploadedFile != null)
            {
                string str= uploadedFile.FileName.Replace(" ", "_");
                string str1 = str.Replace("-", "_");
                // Путь к папке Files
                string path = "/MusicFiles/" + str1; // имя файла

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
                }
                Song s = new();
                Style sStyle = await rep.GetStyle(song.StyleId);
                Artist aArtist = await rep.GetArtist(song.ArtistId);
                s.Name = song.Name;
                s.style =sStyle;
                s.artist = aArtist;
                s.Year = song.Year;
                s.text = song.text;
                s.Album=song.Album; 
                s.file = path;
                if (ModelState.IsValid)
                {
                    try
                    {
                        await rep.AddSong(s);
                        await rep.Save();
                        return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        await putStylesArtists();
                        return View("AddSong", song);
                    }
                }
                else
                {
                    await putStylesArtists();
                    return View("AddSong", song);
                }
            }
            await putStylesArtists();
            return View("AddSong", song);
        }
        public async Task putStylesArtists()
        {
            List<Style> s = await rep.GetStylesList();
            List<Artist> a = await rep.GetArtistsList();
            ViewData["StyleId"] = new SelectList(s, "Id", "Name");
            ViewData["ArtistId"] = new SelectList(a, "Id", "Name");
        }

    }
}
