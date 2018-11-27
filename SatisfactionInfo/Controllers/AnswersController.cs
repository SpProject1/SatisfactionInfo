using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SatisfactionInfo.Models.DTO;
using SatisfactionInfo.Models.Repo.Interfaces;

namespace SatisfactionInfo.Controllers
{
    public class AnswersController : Controller
    {
        private readonly IAnswersRepo answersRepo;

        public AnswersController(IAnswersRepo answersRepo)
        {
            this.answersRepo = answersRepo;
        }
        public async Task<IActionResult> Index()
        {
            var model = await answersRepo.GetList();
            return View(model);
        }      
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Answer")] AnswersDTO item)
        {
            if (ModelState.IsValid)
            {
                await answersRepo.Add(item);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }
        public async Task<IActionResult> Edit(int? id)
        {            
            if (id == null)
            {
                return NotFound();
            }
            var answer = await answersRepo.Get(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(new AnswersDTO { Id = answer.Id, Answer = answer.Answer } );
        }

        // POST: Answers1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Answer")] AnswersDTO answersDTO)
        {  
            if (ModelState.IsValid)
            {
                try
                {
                    await answersRepo.Update(answersDTO);
                }
                catch (Exception)
                {
                    return View("Error");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(answersDTO);
        }
        // POST: Answers1/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await answersRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}