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
        private readonly IUserQuestionnariesRepo userQuestionnariesRepo;     

        public HomeController(IUserQuestionnariesRepo userQuestionnariesRepo)
        {          
            this.userQuestionnariesRepo = userQuestionnariesRepo;            
        }
        public IActionResult Index(InfoDTO info = null)
        {
            ViewBag.HideInput = false;
            ViewBag.Info = info;
            if (info != null && info.Type == InfoDTO.InfoType.Success)           
                ViewBag.HideInput = true;            
            return View();
        }
        public async Task<IActionResult> StartQuestionnarie(QuestionnareCodeDTO item)
        {
            if (ModelState.IsValid)
            {
                //AnswerTypes
                var questionarie = await userQuestionnariesRepo.GetFull(item.Code);
                if (questionarie == null || questionarie.ErrorMessage != null)
                {
                    return RedirectToAction(nameof(Index), new InfoDTO(InfoDTO.InfoType.Error, questionarie.ErrorMessage ?? "Nie znaloziono ankiety."));
                }                
                int randomNumber = DateTime.Now.Second;                
                return View("ShowQuestionnarie", questionarie);
            }
            else
                return RedirectToAction(nameof(Index), new InfoDTO(InfoDTO.InfoType.Error, "Wpisz lub wklej kod ankiety!"));

        }
        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> AddUserQuestionnarie(List<AnsweredDTO> item)
        {
            var result = await userQuestionnariesRepo.AddQuestionnarieAsync(item);
            if (result == "success")
            {
                return Json(new { info = new InfoDTO(InfoDTO.InfoType.Success, "Twoja ankieta została zapisana! Dziękujemy.") });
            }
            return Json(new { info = new InfoDTO(InfoDTO.InfoType.Error, $"Nie udało się dodać ankiety. {result}") });

        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
