using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactionInfo.Models.DTO;
using SatisfactionInfo.Models.Repo.Interfaces;

namespace SatisfactionInfo.Controllers
{
    [Authorize]
    public class UserQuestionnariesController : Controller
    {
        private readonly IUserQuestionnariesRepo userQuestionnariesRepo;

        public UserQuestionnariesController(IUserQuestionnariesRepo userQuestionnariesRepo)
        {
            this.userQuestionnariesRepo = userQuestionnariesRepo;
        }
        public async Task<IActionResult> Index()
        {            
            return View(await userQuestionnariesRepo.GetList());
        }
        public async Task<IActionResult> _QuestionnariesToPrint(int id)
        {
            return View(await userQuestionnariesRepo.Get(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetFiltered(string code, string name, DateTime? date, string description)
        {
            var model = await userQuestionnariesRepo.GetList(code, name, date, description);
            return PartialView("_Questionnaries", model);
        }
    }
}