using CAFEMENUPROJECT.DATA.DataAccess;
using CAFEMENUPROJECT.DATA.Model;
using CAFEMENUPROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace CAFEMENUPROJECT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var data = CategoryDataAccess.GetList().Where(i => i.IsDeleted == false && i.ParentCategoryId == 0).ToList();
            return View(data);
        }

        public PartialViewResult GetCategory(int id)
        {
            if (id == 0)
            {
                var data = CategoryDataAccess.GetList().Where(i => i.IsDeleted == false).ToList();
                return PartialView(data);
            }
            else
            {
                var data = CategoryDataAccess.GetList().Where(i => i.IsDeleted == false && i.ParentCategoryId == id).ToList();
                return PartialView(data);
            }

        }

        public PartialViewResult GetProduct(int id)
        {
            ViewBag.Categories = CategoryDataAccess.GetList().Where(i => i.IsDeleted == false).ToList();
            ViewBag.Currency = Currency();
            //ViewBag.ProductProperties= ProductPropertyDataAccess.GetListAll();
            ViewBag.Properties = PropertyDataAccess.GetList();
         
            
            if (id == 0)
            {
                var data = ProductDataAccess.GetList().Where(i => i.IsDeleted == false).ToList();
                return PartialView(data);
            }
            else
            {
                var data = ProductDataAccess.GetList().Where(i => i.CategoryId == id && i.IsDeleted == false).ToList();
                return PartialView(data);
            }
        }

        public IActionResult GetLoad(int id)
        {
            var data = CategoryDataAccess.GetList().Where(i => i.ParentCategoryId == id).FirstOrDefault().Id;
            return Json(data);
        }

        public List<CurrencyDto> Currency()
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
                Value = String.Format("{0:#.00}", Convert.ToDecimal(EURO_Alis) / 10000)
            });
            currencyDtos.Add(new CurrencyDto()
            {
                Key = "EURO Satis",
                Value = String.Format("{0:#.00}", Convert.ToDecimal(EURO_Satis) / 10000)
            });
            currencyDtos.Add(new CurrencyDto()
            {
                Key = "USD Alis",
                Value = String.Format("{0:#.00}", Convert.ToDecimal(USD_Satis) / 10000)
        });
            currencyDtos.Add(new CurrencyDto()
            {
                Key = "USD Satis",
                Value = String.Format("{0:#.00}", Convert.ToDecimal(USD_Alis) / 10000)
            });

            //ViewBag.List = currencyDtos;

            return currencyDtos;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
