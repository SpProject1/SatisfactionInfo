using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SatisfactionInfo.Models;
using SatisfactionInfo.Models.DTO;
using SatisfactionInfo.Models.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVUserQuestionnarieRepo vUserQuestionnarieRepo;
        private readonly IUserQuestionnariesRepo userQuestionnariesRepo;     

        public HomeController(IVUserQuestionnarieRepo vUserQuestionnarieRepo, IUserQuestionnariesRepo userQuestionnariesRepo)
        {
            this.vUserQuestionnarieRepo = vUserQuestionnarieRepo;
            this.userQuestionnariesRepo = userQuestionnariesRepo;            
        }
        public IActionResult Index(InfoDTO info = null)
        {
            ViewBag.Info = info;
            return View();
        }
        public async Task<IActionResult> StartQuestionnarie(QuestionnareCodeDTO item)
        {
            if (ModelState.IsValid)
            {
                //AnswerTypes
                var questionarie = await vUserQuestionnarieRepo.Get(item.Code);
                if (questionarie == null)
                {
                    return RedirectToAction(nameof(Index), new InfoDTO(InfoDTO.InfoType.Error, "Nie znaloziono ankiety."));
                }
                //questionarie.Url = Request.GetDisplayUrl() + (Request.GetDisplayUrl().EndsWith("StartQuestionnarie") ? $"/?code={item.Code}" : "");
                int randomNumber = DateTime.Now.Second;                
                return View("ShowQuestionnarie", questionarie);
            }
            else
                return RedirectToAction(nameof(Index), new InfoDTO(InfoDTO.InfoType.Error, "Wpisz lub wklej kod ankiety!"));

        }
        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> AddUserQuestionnarie(List<AnsweredDTO> model)
        {
            var result = await userQuestionnariesRepo.AddQuestionnarieAsync(model);
            if (result == "success")
            {
                return Json(new { info = new InfoDTO(InfoDTO.InfoType.Success, "Twoja ankieta została zapisana! Dziękujemy.") });
            }
            return Json(new { info = new InfoDTO(InfoDTO.InfoType.Error, $"Nie udało się dodać ankiety. {result}") });

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
