using Microsoft.EntityFrameworkCore;
using SatisfactionInfo.Models.DAL.SQL;
using SatisfactionInfo.Models.DTO;
using SatisfactionInfo.Models.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.Repo.SQL
{
    public class AnswersRepo : IAnswersRepo
    {
        private readonly SatisfactionInfoContext db;

        public AnswersRepo(SatisfactionInfoContext _db)
        {
            this.db = _db;
        }
        public async Task<List<AnswersDTO>> GetList()
        {
            return await db.Answers
            .Select(answer => new AnswersDTO
            {
                Id = answer.Id,
                Answer = answer.Answer
            }).OrderByDescending(a => a.Id).ToListAsync();

        }
        public async Task<AnswersDTO> Get(int? id)
        {
            return await db.Answers
            .Where(answer => answer.Id == id)
            .Select(answer => new AnswersDTO
            {
                Id = answer.Id,
                Answer = answer.Answer
            }).FirstOrDefaultAsync();
        }
        public async Task Add(AnswersDTO item)
        {
            var answer = new Answers
            {
                Id = item.Id,
                Answer = item.Answer
            };
            db.Answers.Add(answer);
            await db.SaveChangesAsync();
        }
        public async Task Update(AnswersDTO item)
        {
            var answer = await db.Answers.FindAsync(item.Id);
            if (answer != null)
            {
                answer.Answer = item.Answer;
                db.Entry(answer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
        public async Task Delete(int? id)
        {
            var answer = id != null ? await db.Answers.FindAsync(id) : null;
            if (answer != null)
            {
                db.Entry(answer).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                await db.SaveChangesAsync();
            }
        }
    }
}
