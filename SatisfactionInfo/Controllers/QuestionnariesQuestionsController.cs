using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SatisfactionInfo.Models.DAL.SQL;

namespace SatisfactionInfo.Controllers
{
    public class QuestionnariesQuestionsController : Controller
    {
        private readonly SatisfactionInfoContext _context;

        public QuestionnariesQuestionsController(SatisfactionInfoContext context)
        {
            _context = context;
        }

        // GET: QuestionnariesQuestions
        public async Task<IActionResult> Index()
        {
            var satisfactionInfoContext = _context.QuestionnariesQuestion.Include(q => q.Question).Include(q => q.Questionnarie);
            return View(await satisfactionInfoContext.ToListAsync());
        }

        // GET: QuestionnariesQuestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnariesQuestion = await _context.QuestionnariesQuestion
                .Include(q => q.Question)
                .Include(q => q.Questionnarie)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (questionnariesQuestion == null)
            {
                return NotFound();
            }

            return View(questionnariesQuestion);
        }

        // GET: QuestionnariesQuestions/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Question");
            ViewData["QuestionnarieId"] = new SelectList(_context.Questionnaries, "Id", "Name");
            return View();
        }

        // POST: QuestionnariesQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionnarieId,QuestionId,QuestionNumber")] QuestionnariesQuestion questionnariesQuestion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionnariesQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Question", questionnariesQuestion.QuestionId);
            ViewData["QuestionnarieId"] = new SelectList(_context.Questionnaries, "Id", "Name", questionnariesQuestion.QuestionnarieId);
            return View(questionnariesQuestion);
        }

        // GET: QuestionnariesQuestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnariesQuestion = await _context.QuestionnariesQuestion.FindAsync(id);
            if (questionnariesQuestion == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Question", questionnariesQuestion.QuestionId);
            ViewData["QuestionnarieId"] = new SelectList(_context.Questionnaries, "Id", "Name", questionnariesQuestion.QuestionnarieId);
            return View(questionnariesQuestion);
        }

        // POST: QuestionnariesQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionnarieId,QuestionId,QuestionNumber")] QuestionnariesQuestion questionnariesQuestion)
        {
            if (id != questionnariesQuestion.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionnariesQuestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionnariesQuestionExists(questionnariesQuestion.QuestionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Question", questionnariesQuestion.QuestionId);
            ViewData["QuestionnarieId"] = new SelectList(_context.Questionnaries, "Id", "Name", questionnariesQuestion.QuestionnarieId);
            return View(questionnariesQuestion);
        }

        // GET: QuestionnariesQuestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnariesQuestion = await _context.QuestionnariesQuestion
                .Include(q => q.Question)
                .Include(q => q.Questionnarie)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (questionnariesQuestion == null)
            {
                return NotFound();
            }

            return View(questionnariesQuestion);
        }

        // POST: QuestionnariesQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionnariesQuestion = await _context.QuestionnariesQuestion.FindAsync(id);
            _context.QuestionnariesQuestion.Remove(questionnariesQuestion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionnariesQuestionExists(int id)
        {
            return _context.QuestionnariesQuestion.Any(e => e.QuestionId == id);
        }
    }
}
