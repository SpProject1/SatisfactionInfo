using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactionInfo.Models.DTO;
using SatisfactionInfo.Models.Repo.Interfaces;
using TagHelpers;

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
        public async Task<IActionResult> Index(int pageId = 1,int? pageSizeLocal = null)
        {
            var model = await userQuestionnariesRepo.GetList(page: pageId, pageSizeLocal: pageSizeLocal);
            ViewBag.PageInfo = userQuestionnariesRepo.PageInfo;
            return View(model);
        }
        public async Task<IActionResult> _QuestionnariesToPrint(int id)
        {
            ViewBag.ToPrint = true;
            return View(await userQuestionnariesRepo.Get(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetFiltered(int pageId, string code, string name, string date, string description)
        {
            var model = await userQuestionnariesRepo.GetList(pageId, code, name, date, description);
            ViewBag.PageInfo = userQuestionnariesRepo.PageInfo;
            return PartialView("_Questionnaries", model);
        }
    }
}