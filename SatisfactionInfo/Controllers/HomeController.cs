using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SatisfactionInfo.Models;
using SatisfactionInfo.Models.DTO;
using SatisfactionInfo.Models.Repo.Interfaces;

namespace SatisfactionInfo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVUserQuestionarieRepo vUserQuestionarieRepo;

        public HomeController(IVUserQuestionarieRepo vUserQuestionarieRepo)
        {
            this.vUserQuestionarieRepo = vUserQuestionarieRepo;
        }
        public IActionResult Index(string info = null)
        {           
            ViewBag.ErrorMessage = info;   
            return View();
        }
        public IActionResult StartQuestionarie(QuestionareCodeDTO item)
        {
            if (ModelState.IsValid)
            {
                var questionarie = vUserQuestionarieRepo.Get(item.Code);
                if (questionarie == null)
                {
                    return RedirectToAction(nameof(Index), new { info = "Nie znaloziono ankiety." });
                }
                return View("ShowQuestionarie", questionarie);
            }
            else
                return RedirectToAction(nameof(Index), new { info = "Wpisz lub wklej kod ankiety!" });

        }      
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
