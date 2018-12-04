using Microsoft.AspNetCore.Http.Extensions;
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
        public async Task<IActionResult> Index(string info = null)
        {
            //userQuestionnariesRepo.Add(new UserQuestionnariesDTO
            //{
            //    Code = "aaaaa",
            //    Name = "Próbna",
            //    UserQuestionnarieAnswersDTOs = new List<UserQuestionnarieAnswersDTO>
            //     {
            //        new UserQuestionnarieAnswersDTO
            //        {
            //            Code = "aaaaa",
            //            AddWhy = true,
            //            AddWhyBody = "coś",
            //            AddWhyName = "opis pola",
            //            Answered = "1",
            //            AnswerType = "jednokrotny",
            //            AvailableAnswers="1;2;3",
            //            Question = "czy tak?",
            //            QuestionNomber = 1
            //        },
            //         new UserQuestionnarieAnswersDTO
            //        {
            //            Code = "aaaaa",
            //            AddWhy = true,
            //            AddWhyBody = "coś",
            //            AddWhyName = "opis pola",
            //            Answered = "2",
            //            AnswerType = "jednokrotny",
            //            AvailableAnswers="1;2;3",
            //            Question = "czy tak?",
            //            QuestionNomber = 2
            //        }
            //     }

            //});
            var x = await userQuestionnariesRepo.GetList("aaaaa");
            ViewBag.ErrorMessage = info;
            return View();
        }
        public async Task<IActionResult> StartQuestionnarie(QuestionnareCodeDTO item)
        {
            if (ModelState.IsValid)
            {
                var questionarie = await vUserQuestionnarieRepo.Get(item.Code);
                if (questionarie == null)
                {
                    return RedirectToAction(nameof(Index), new { info = "Nie znaloziono ankiety." });
                }
                //questionarie.Url = Request.GetDisplayUrl() + (Request.GetDisplayUrl().EndsWith("StartQuestionnarie") ? $"/?code={item.Code}" : "");
                return View("ShowQuestionnarie", questionarie);
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
