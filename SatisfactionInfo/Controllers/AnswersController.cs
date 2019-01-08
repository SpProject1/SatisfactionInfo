using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SatisfactionInfo.Models.DAL.SQL;
using SatisfactionInfo.Models.DTO;
using SatisfactionInfo.Models.Repo.Interfaces;

namespace SatisfactionInfo.Controllers
{
    [Authorize]
    public class AnswersController : Controller
    {
        private readonly IAnswersRepo _repo;

        public AnswersController(IAnswersRepo repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetList());
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(AnswersDTO item)
        {
            if (ModelState.IsValid)
            {
                if (item.Id > 0)
                    await _repo.Update(item);
                else
                    await _repo.Add(item);                
                return PartialView("_Answers", await _repo.GetList());
            }
            return Content("Wypełnij wymagane pole");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {            
            await _repo.Delete(id);
            return Content("success");
        }
    }
}
