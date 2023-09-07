using Microsoft.AspNetCore.Mvc;
using MusikPortal.Models;
using System.Security.Cryptography;
using System.Text;
using MusikPortal.Repository;
using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;

namespace MusikPortal.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService userService;
        private readonly ISaltService saltService;
        public LoginController(IUserService u, ISaltService s)
        {
            userService = u;
            saltService = s;
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterModel user)
        {
            try
            {
                if (Convert.ToInt32(user.age) < 0 || Convert.ToInt32(user.age) > 99)
                    ModelState.AddModelError("age", "uncorrectly age");
            }
            catch { ModelState.AddModelError("age", "uncorrectly age"); }
                if (ModelState.IsValid)
                {
                    if (await userService.GetUser(user.Login) != null)
                    {
                        ModelState.AddModelError("login", "this login already exists");
                        return View(user);
                    }
                    if (await userService.GetEmail(user.email) != null)
                    {
                        ModelState.AddModelError("email", "this email is already registred");
                        return View(user);
                    }
                UserDTO u = new();
                    u.Name = user.Login;
                    u.Age = user.age;
                    u.email = user.email;
                    byte[] saltbuf = new byte[16];
                    RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                    randomNumberGenerator.GetBytes(saltbuf);
                    StringBuilder sb = new StringBuilder(16);
                    for (int i = 0; i < 16; i++)
                        sb.Append(string.Format("{0:X2}", saltbuf[i]));
                    string salt = sb.ToString();
                    SaltDTO s = new();
                    s.salt = salt;
                    string password = salt + user.Password;
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                    //bool passwordsMatch = BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPasswordFromDatabase); для проверки совпадения           
                    u.Password = hashedPassword;
                    u.Level = 0;
                    try
                    {
                        await userService.AddUser(u);                       
                        s.userId = u.Id;
                        await saltService.AddSalt(s);                       
                    }
                    catch { }
                    return RedirectToAction("Login");
                }
            return View(user);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel user)
        {
           
            if (ModelState.IsValid)
            {

                var u = await userService.GetUser(user.Login);
                var s = await saltService.GetSalt(u);
                {
                   
                    if (u != null && s != null)
                    {
                        string conf = s.salt + user.Password;
                        if (BCrypt.Net.BCrypt.Verify(conf, u.Password))
                        {

                            HttpContext.Session.SetString("login", user.Login);
                            if (u.Level == 1)
                                HttpContext.Session.SetString("level", "level");
                            if (u.Level==2)
                              HttpContext.Session.SetString("admin", "admin");// создание сессионной переменной
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "login/password  not correct");
                            return View(user);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "login/password  not correct");
                        return View(user);
                    }
                }
            }
            return View(user);
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            bool isEmailInUse =await userService.CheckEmail(email);
            return Json(!isEmailInUse);
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsLoginInUse( string login)
        {

            bool isUnique = await userService.GetLogins(login);
            return Json(isUnique);
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear(); // очищается сессия
            return RedirectToAction("Index", "Home");
        }
     
    }
}
