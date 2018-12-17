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

namespace SatisfactionInfo.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly SatisfactionInfoContext _context;

        public QuestionsController(SatisfactionInfoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "Answer");
            ViewData["AnswerType"] = new SelectList(_context.AnswerTypes, "AnswerType", "AnswerType");
            var model = _context
                .Questions.OrderByDescending(a => a.Id)
                .Include(q => q.AnswerTypeNavigation)
                .Include(qa => qa.QuestionsAnswer)
                .ThenInclude(a => a.Answer);
            return View(await model.ToListAsync());
        }
        public async Task<PartialViewResult> _Answers(int questionId)
        {
            ViewBag.QuestionID = questionId;
            var model = _context.QuestionsAnswer.Where(a => a.QuestionId == questionId).Include(a => a.Answer);
            return PartialView(nameof(_Answers), await model.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Questions item)
        {
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "Answer");
            ViewData["AnswerType"] = new SelectList(_context.AnswerTypes, "AnswerType", "AnswerType");
            if (ModelState.IsValid)
            {
                if (item.Id > 0)
                    _context.Update(item);
                else
                    _context.Add(item);
                await _context.SaveChangesAsync();
                var model = _context
                .Questions.OrderByDescending(a => a.Id)
                .Include(q => q.AnswerTypeNavigation)
                .Include(qa => qa.QuestionsAnswer)
                .ThenInclude(a => a.Answer);
                return PartialView("_Questions", await model.ToListAsync());
            }
            return Content("Wypełnij wszystkie wymagane pola");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.QuestionsAnswer.Any(a => a.QuestionId == id))
                return Content("Nie mozna usunąć pytania - posiada odpowiedzi.");
            var questions = await _context.Questions.FindAsync(id);            
            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();
            return Content("success");
        }
        [HttpPost]
        public async Task<IActionResult> AddQuestionAnswer(QuestionsAnswer item)
        {
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "Answer");
            ViewData["AnswerType"] = new SelectList(_context.AnswerTypes, "AnswerType", "AnswerType");
            if (ModelState.IsValid)
            {
                if (!questionsAnswerExists(item.QuestionId, item.AnswerId))
                {
                    _context.Add(item);
                    await _context.SaveChangesAsync();
                    var model = new QuestionAnswerViewModel();
                    model.List = await _context.QuestionsAnswer.Where(a => a.QuestionId == item.QuestionId).Include(q => q.Answer).ToListAsync();
                    model.QuestionId = item.QuestionId;
                    return PartialView("_Answers", model);
                }
                return Content("exists");

            }
            return Content("Wypełnij wszystkie wymagane pola");
        }
        [HttpDelete]//DeleteQuestionAnswer
        public async Task<IActionResult> DeleteQuestionAnswer(QuestionsAnswer item)
        {
            var questionsAnswer = await _context.QuestionsAnswer.Where(a => a.AnswerId == item.AnswerId && a.QuestionId == a.QuestionId).FirstOrDefaultAsync();
            _context.QuestionsAnswer.Remove(questionsAnswer);
            await _context.SaveChangesAsync();
            return Content("success");
        }
        private bool questionsAnswerExists(int qid, int aid)
        {
            return _context.QuestionsAnswer.Any(e => e.QuestionId == qid && e.AnswerId == aid);
        }
    }
}
