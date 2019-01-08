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
    public class QuestionsController : Controller
    {
        private readonly IQuestionsRepo _repo;

        public QuestionsController(IQuestionsRepo repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            setViewData();          
            return View(await _repo.GetList());
        }
        public async Task<PartialViewResult> _Answers(int questionId)
        {
            ViewBag.QuestionID = questionId;          
            return PartialView(nameof(_Answers), await _repo.GetListQuestionsAnswer(questionId));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(QuestionsDTO item)
        {
            setViewData();
            if (ModelState.IsValid)
            {
                if (item.Id > 0)
                    await _repo.Update(item);
                else
                  await  _repo.Add(item);
               
                return PartialView("_Questions", await _repo.GetList());
            }
            return Content("Wypełnij wszystkie wymagane pola");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (_repo.QuestionsAnswerExists(id))
                return Content("Nie mozna usunąć pytania - posiada odpowiedzi.");            
            await _repo.Delete(id);
            return Content("success");
        }

        //questionAnswers
        [HttpPost]
        public async Task<IActionResult> AddQuestionAnswer(QuestionsAnswerDTO item)
        {
            setViewData();
            if (ModelState.IsValid)
            {
                if (!_repo.QuestionsAnswerExists(item.QuestionId, item.AnswerId))
                {
                    await _repo.AddQuestionAnswer(item);
                    var model = new QuestionAnswerViewModel();
                    model.List = await _repo.GetListQuestionsAnswer(item.QuestionId);
                    model.QuestionId = item.QuestionId;
                    return PartialView("_Answers", model);
                }
                return Content("exists");

            }
            return Content("Wypełnij wszystkie wymagane pola");
        }
        [HttpDelete]//DeleteQuestionAnswer
        public async Task<IActionResult> DeleteQuestionAnswer(QuestionsAnswerDTO item)
        {
            await _repo.DeleteQuestionAnswer(item);
            return Content("success");
        }     
        private void setViewData()
        {
            ViewData["AnswerId"] = new SelectList(_repo.GetAnswersList(), "Id", "Answer");
            ViewData["AnswerType"] = new SelectList(_repo.GetAnswerTypesList(), "AnswerType", "AnswerType");
        }
    }
}
