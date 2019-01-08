using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SatisfactionInfo.Helpers;
using SatisfactionInfo.Models.DAL.SQL;
using SatisfactionInfo.Models.DTO;

namespace SatisfactionInfo.Controllers
{
    [Authorize]
    public class QuestionnariesController : Controller
    {
        private readonly SatisfactionInfoContext _context;

        public QuestionnariesController(SatisfactionInfoContext context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index()
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Question");
            return View(await _context.Questionnaries
                    .Include(q => q.QuestionnariesQuestion)
                    .ThenInclude(q => q.Question)
                    .OrderByDescending(a => a.Id)
                    .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Questionnaries item)
        {
            if (ModelState.IsValid)
            {
                ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Question");
                if (item.Id > 0)
                    _context.Update(item);
                else
                {
                    string guid = GuidHelper.GetShortGuid;
                    while (await _context.Questionnaries.Where(a => a.Code == guid).CountAsync() > 0)
                        guid = GuidHelper.GetShortGuid;
                    item.Code = guid;
                    item.Active = true;
                    _context.Add(item);
                }
                await _context.SaveChangesAsync();
                var model = _context.Questionnaries
                    .Include(q => q.QuestionnariesQuestion)
                    .ThenInclude(q => q.Question)
                    .OrderByDescending(a => a.Id);
                return PartialView("_Questionnaries", await model.ToListAsync());
            }
            return Content("Wypełnij wszystkie wymagane pola");
        }
        [HttpPost]
        public async Task<IActionResult> Clone(int id)
        {
            if (ModelState.IsValid)
            {
                ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Question");
                if (id > 0)
                {
                    string guid = GuidHelper.GetShortGuid;
                    while (await _context.Questionnaries.Where(a => a.Code == guid).CountAsync() > 0)
                        guid = GuidHelper.GetShortGuid;
                    var item = await _context.Questionnaries.Where(a => a.Id == id).FirstOrDefaultAsync();
                    var newItem = new Questionnaries
                    {
                        Active = true,
                        Code = guid,
                        MaxAnswers = item.MaxAnswers,
                        Description = item.Description,
                        Name = item.Name + "_kopia"
                    };
                    _context.Add(newItem);
                    await _context.SaveChangesAsync();
                    var questions = await _context.QuestionnariesQuestion.Include(q => q.Question).Where(a => a.QuestionnarieId == id).OrderBy(a => a.QuestionNumber).ToListAsync();
                    foreach (var question in questions)
                    {
                        question.QuestionnarieId = newItem.Id;
                        _context.Add(question);
                    }
                    _context.SaveChanges();
                }
                var model = _context.Questionnaries
                    .Include(q => q.QuestionnariesQuestion)
                    .ThenInclude(q => q.Question)
                    .OrderByDescending(a => a.Id);
                return PartialView("_Questionnaries", await model.ToListAsync());
            }
            return Content("Wypełnij wszystkie wymagane pola");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.QuestionnariesQuestion.Any(a => a.QuestionnarieId == id))
                return Content("Nie mozna usunąć ankiety - posiada pytania.");
            var questionnaries = await _context.Questionnaries.FindAsync(id);
            _context.Questionnaries.Remove(questionnaries);
            await _context.SaveChangesAsync();
            return Content("success");
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateQuestionnarieQuestion(QuestionnariesQuestion item)
        {
            if (ModelState.IsValid)
            {
                ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Question");
                if (QuestionnariesQuestionExists(item.QuestionnarieId, item.QuestionId))
                {
                    var temp = await _context.QuestionnariesQuestion.Where(a => a.QuestionId == item.QuestionId && a.QuestionnarieId == item.QuestionnarieId).FirstOrDefaultAsync();
                    if (temp != null && temp.QuestionNumber == item.QuestionNumber)
                        return Content("Pytanie istnieje w ankiecie");
                    temp.QuestionNumber = item.QuestionNumber;
                    _context.Update(temp);
                }
                else
                    _context.Add(item);
                await _context.SaveChangesAsync();
                var model = new QuestionnarieQuestionsViewModel();
                model.QuestionnarieId = item.QuestionnarieId;
                model.List = await _context.QuestionnariesQuestion.Include(q => q.Question).Where(a => a.QuestionnarieId == item.QuestionnarieId).OrderBy(a => a.QuestionNumber).ToListAsync();
                return PartialView("_Questions", model);
            }
            return Content("Wypełnij wszystkie wymagane pola");

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteQuestionnarieQuestion(QuestionnariesQuestion item)
        {
            var questionnaries = await _context.QuestionnariesQuestion.Where(a => a.QuestionId == item.QuestionId && a.QuestionnarieId == item.QuestionnarieId).FirstOrDefaultAsync();
            _context.QuestionnariesQuestion.Remove(questionnaries);
            await _context.SaveChangesAsync();
            return Content("success");
        }
        private bool QuestionnariesQuestionExists(int id, int questionid)
        {
            return _context.QuestionnariesQuestion.Any(e => e.QuestionnarieId == id && e.QuestionId == questionid);
        }
    }
}
