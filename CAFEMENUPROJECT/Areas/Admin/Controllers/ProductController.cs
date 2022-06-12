using CAFEMENUPROJECT.DATA.DataAccess;
using CAFEMENUPROJECT.DATA.Entity;
using CAFEMENUPROJECT.DATA.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionControl]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var data = ProductDataAccess.GetList().Where(i => i.IsDeleted == false).ToList();
            ViewBag.Categories = CategoryDataAccess.GetList().Where(i => i.IsDeleted == false).ToList();
            ViewBag.Users = UserDataAccess.GetList();
            return View(data);
        }

        public IActionResult Add()
        {
            var categories = CategoryDataAccess.GetList().Where(i => i.IsDeleted == false).ToList();

            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product model, IFormFile dosya)
        {
            var categories = CategoryDataAccess.GetList().Where(i => i.IsDeleted == false).ToList();

            ViewBag.Categories = categories;

            if (dosya == null)
            {

                ViewBag.Hata = "Lütfen dosya seçiniz";
                return View(model);
            }

            using (var image = Image.FromStream(dosya.OpenReadStream()))
            {
                // use image.Width and image.Height
                if (image.Width - image.Height > 100 || image.Height - image.Width > 100)
                {
                    ViewBag.Hata = "Fotograf Oranını 1:1 olmalıdır";
                    return View(model);
                }
            }

            string dosyaYolu = Path.GetFileName(dosya.FileName);

            var randomName = ($"{Guid.NewGuid()}{dosyaYolu}");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await dosya.CopyToAsync(stream);
            }

            var user = HttpContext.Session.User();

            model.ImagePath = randomName;
            model.IsDeleted = false;
            model.CreatedDate = DateTime.Now;
            model.CreatorUserId = user.Id;


            if (model != null)
            {
                var sonuc = ProductDataAccess.Add(model);
            }

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Update(int id)
        {
            ViewBag.Categories = CategoryDataAccess.GetList().Where(i => i.IsDeleted == false).ToList();
            var data = ProductDataAccess.GetById(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product model, IFormFile dosya)
        {
            ViewBag.Categories = CategoryDataAccess.GetList().Where(i => i.IsDeleted == false).ToList();

            if (dosya != null)
            {
                string dosyaYolu = Path.GetFileName(dosya.FileName);

                var randomName = ($"{Guid.NewGuid()}{dosyaYolu}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await dosya.CopyToAsync(stream);
                }

                using (var image = Image.FromStream(dosya.OpenReadStream()))
                {
                    // use image.Width and image.Height
                    if (image.Width - image.Height > 100 || image.Height - image.Width > 100)
                    {
                        ViewBag.Hata = "Fotograf Oranını 1:1 olmalıdır";
                        return View(model);
                    }
                }

                model.ImagePath = randomName;
            }
            else
            {
                var Product = ProductDataAccess.GetById(model.Id);
                model.ImagePath = Product.ImagePath;
            }

            if (model != null)
            {
                var sonuc = ProductDataAccess.Update(model);
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = ProductDataAccess.Delete(id);
            return Json(data);
        }

        public IActionResult AddProperty(int id)
        {
            ViewBag.ProductProperties = ProductPropertyDataAccess.GetList(id);
            ViewBag.Properties = PropertyDataAccess.GetList();
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

    }
}
