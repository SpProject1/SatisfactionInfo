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
    public class VUserQuestionariesController : Controller
    {
        private readonly SatisfactionInfoContext _context;

        public VUserQuestionariesController()
        {
            _context = new SatisfactionInfoContext();
        }

        // GET: VUserQuestionaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.VUserQuestionarie.ToListAsync());
        }

        // GET: VUserQuestionaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vUserQuestionarie = await _context.VUserQuestionarie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vUserQuestionarie == null)
            {
                return NotFound();
            }

            return View(vUserQuestionarie);
        }

        // GET: VUserQuestionaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VUserQuestionaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Question,AddWhy,Answer,Weight,AnswerType")] VUserQuestionarie vUserQuestionarie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vUserQuestionarie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vUserQuestionarie);
        }

        // GET: VUserQuestionaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vUserQuestionarie = await _context.VUserQuestionarie.FindAsync(id);
            if (vUserQuestionarie == null)
            {
                return NotFound();
            }
            return View(vUserQuestionarie);
        }

        // POST: VUserQuestionaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Question,AddWhy,Answer,Weight,AnswerType")] VUserQuestionarie vUserQuestionarie)
        {
            if (id != vUserQuestionarie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vUserQuestionarie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VUserQuestionarieExists(vUserQuestionarie.Id))
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
            return View(vUserQuestionarie);
        }

        // GET: VUserQuestionaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vUserQuestionarie = await _context.VUserQuestionarie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vUserQuestionarie == null)
            {
                return NotFound();
            }

            return View(vUserQuestionarie);
        }

        // POST: VUserQuestionaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vUserQuestionarie = await _context.VUserQuestionarie.FindAsync(id);
            _context.VUserQuestionarie.Remove(vUserQuestionarie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VUserQuestionarieExists(int id)
        {
            return _context.VUserQuestionarie.Any(e => e.Id == id);
        }
    }
}
