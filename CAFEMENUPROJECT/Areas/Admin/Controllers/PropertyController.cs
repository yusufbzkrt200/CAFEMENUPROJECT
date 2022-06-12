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
    public class PropertyController : Controller
    {
        public IActionResult Index(int id)
        {
            var data = ProductDataAccess.GetById(id);
            return View(data);
        }

        public PartialViewResult GetList(int id)
        {
            ViewBag.ProductProperties = ProductPropertyDataAccess.GetList(id);
            ViewBag.Properties = PropertyDataAccess.GetList();
            ViewBag.Id = id;

            return PartialView("GetList");
        }

        [HttpPost]
        public IActionResult Add(Property model, int id)
        {
            var data = PropertyDataAccess.Add(model, id);
            return Json(data);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = PropertyDataAccess.Delete(id);
            return Json(data);
        }

        public PartialViewResult Update(int id)
        {
            var data = PropertyDataAccess.GetById(id);
            return PartialView("Update", data);
        }

        [HttpPost]
        public IActionResult Update(Property model)
        {
            var data = PropertyDataAccess.Update(model);
            return Json(data);
        }
    }
}
