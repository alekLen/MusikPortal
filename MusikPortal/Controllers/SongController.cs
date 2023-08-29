using Microsoft.AspNetCore.Mvc;
using MusikPortal.Repository;
using MusikPortal.Models;
using System.IO;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,style,artist,Year,file")] Song song, IFormFile f)
        {
            if (f == null)
                ModelState.AddModelError("", "вы не добавили файл");
            DateTime today = DateTime.Today;
            int currentYear = today.Year;
            if (Convert.ToInt32(song.Year) < 1895 || Convert.ToInt32(song.Year) > currentYear)
                ModelState.AddModelError("", "не корректный год");
            if (f != null)
            {
                // Путь к папке Files
                string path = "/MusikFiles/" + f.FileName; // имя файла

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await f.CopyToAsync(fileStream); // копируем файл в поток
                }
                Song s = new();
                s.Name = song.Name;
                s.style = song.style;
                s.artist = song.artist;
                s.Year = song.Year;
                s.text = song.text;
                s.file = path;
                if (ModelState.IsValid)
                {
                    try
                    {
                       await rep.AddSong(s);
                       await rep.Save();
                        return RedirectToAction(nameof(Index));
                    }
                    catch
                    {
                        return View(song);
                    }
                }
                else
                    return View(song);
            }
            return View(song);
        }

    }
}
