using CAFEMENUPROJECT.DATA.DataAccess;
using CAFEMENUPROJECT.DATA.Entity;
using CAFEMENUPROJECT.DATA.Helper;
using CAFEMENUPROJECT.DATA.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        [SessionControl]
        public IActionResult Index()
        {
            var data = UserDataAccess.GetList();
            return View(data);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var data = UserDataAccess.Login(username, password);
            if (data != null)
            {
                var session = new SessionDto()
                {
                    Id = data.Id,
                    Name = data.Name,
                    SurName = data.Surname,
                    UserName = data.Username,
                    HashPassword = data.HashPassword,
                    SaltPassword = data.SaltPassword,
                };


                HttpContext.Session.SetString("_User", JsonConvert.SerializeObject(session));
                return RedirectToAction("Index", "Home");

            }
            ViewBag.Hata = "Bir Hata Oluştu Tekrar Deneyiniz";
            return View();
        }

        public IActionResult LogOut()
        {

            HttpContext.Session.SetString("_User", string.Empty);
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }

        [SessionControl]
        [HttpPost]
        public IActionResult Add(User model)
        {
            var data = UserDataAccess.Add(model);
            return Json(data);
        }

        [SessionControl]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = UserDataAccess.Delete(id);
            return Json(data);
        }

        [SessionControl]
        public PartialViewResult Update(int id)
        {
            var data = UserDataAccess.GetById(id);
            return PartialView("Update", data);
        }

        [SessionControl]
        [HttpPost]
        public IActionResult Update(User model)
        {
            var data = UserDataAccess.Update(model);
            return Json(data);
        }

        [SessionControl]
        public IActionResult Details(int id)
        {
            var data = UserDataAccess.GetById(id);
            return View(data);
        }
    }
}
