using CAFEMENUPROJECT.DATA.DataAccess;
using CAFEMENUPROJECT.DATA.Entity;
using CAFEMENUPROJECT.DATA.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionControl]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Users = UserDataAccess.GetList();

            var data = CategoryDataAccess.GetList().Where(i=>i.IsDeleted!=true);
            return View(data);
        }

        [HttpPost]
        public IActionResult Add(Category model)
        {
            var user = HttpContext.Session.User();
            var data = CategoryDataAccess.Add(model,user);
            return Json(data);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = CategoryDataAccess.Delete(id);
            return Json(data);
        }

        public PartialViewResult Update(int id)
        {

            var categories = CategoryDataAccess.GetList().Where(i=>i.IsDeleted==false).ToList();
            var data = CategoryDataAccess.GetById(id);

            ViewBag.Categories = categories;
            return PartialView("Update", data);
        }

        [HttpPost]
        public IActionResult Update(Category model)
        {
            var data = CategoryDataAccess.Update(model);
            return Json(data);
        }

    }
}
