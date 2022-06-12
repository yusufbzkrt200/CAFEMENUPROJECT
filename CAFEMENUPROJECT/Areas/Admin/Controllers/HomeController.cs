using CAFEMENUPROJECT.DATA.DataAccess;
using CAFEMENUPROJECT.DATA.Helper;
using CAFEMENUPROJECT.DATA.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace CAFEMENUPROJECT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionControl]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var Products = ProductDataAccess.GetList().Where(i => i.IsDeleted == false).ToList();
            var Categories = CategoryDataAccess.GetList().Where(i => i.IsDeleted == false).ToList();
            ViewBag.Categories = Categories;

            ViewBag.TotalProducts = Products.Count();
            ViewBag.TotalCategories = Categories.Count();

            var data = Products
                .GroupBy(x => x.CategoryId)
                .Select(y => new CategoryDto { CategoryId = y.Key, Total = y.Count() });

            return View(data);
        }

        public PartialViewResult Currency()
        {
            string bugun = "http://www.tcmb.gov.tr/kurlar/today.xml";

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(bugun);

            string EURO_Alis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            string EURO_Satis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;

            string USD_Alis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            string USD_Satis = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;

            List<CurrencyDto> currencyDtos = new List<CurrencyDto>();
            currencyDtos.Add(new CurrencyDto()
            {
                Key = "EURO Alis",
                Value = EURO_Alis
            });
            currencyDtos.Add(new CurrencyDto()
            {
                Key = "EURO Satis",
                Value = EURO_Satis
            });
            currencyDtos.Add(new CurrencyDto()
            {
                Key = "USD Alis",
                Value = USD_Alis
            });
            currencyDtos.Add(new CurrencyDto()
            {
                Key = "USD Satis",
                Value = USD_Satis
            });

            ViewBag.List = currencyDtos;

            return PartialView("Currency");
        }
    }
}
