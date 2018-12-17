using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SatisfactionInfo.Models.DAL.SQL;

namespace SatisfactionInfo.Controllers
{
    [Authorize]
    public class AnswersController : Controller
    {
        private readonly SatisfactionInfoContext _context;

        public AnswersController(SatisfactionInfoContext context)
        {
            _context = context;
        }        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Answers.OrderByDescending(a => a.Id).ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Answers item)
        {
            if (ModelState.IsValid)
            {
                if (item.Id > 0)                
                    _context.Update(item);                
                else
                    _context.Add(item);
                await _context.SaveChangesAsync();
                return PartialView("_Answers", await _context.Answers.OrderByDescending(a => a.Id).ToListAsync());
            }
            return Content("Wypełnij wymagane pole");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var answers = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answers);
            await _context.SaveChangesAsync();
            return Content("success");
        }
    }
}
