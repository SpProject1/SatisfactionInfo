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
    public class VUserQuestionarieRepo : IVUserQuestionarieRepo
    {
        public async Task<List<VUserQuestionarieDTO>> GetList()
        {
            using (SatisfactionInfoContext db = new SatisfactionInfoContext())
            {
                return await db.VUserQuestionarie.Select(a => new VUserQuestionarieDTO
                {
                    Id = a.Id,
                    AddWhy = a.AddWhy,
                    Answer = a.Answer,
                    AnswerType = a.AnswerType,
                    Name = a.Name,
                    Question = a.Question,
                    Weight = a.Weight,
                    Code = a.Code
                }).ToListAsync();
            }
        }

        public async Task<List<VUserQuestionarieDTO>> GetList(string code)
        {
            using (SatisfactionInfoContext db = new SatisfactionInfoContext())
            {
                return await db.VUserQuestionarie.Select(a => new VUserQuestionarieDTO
                {
                    Id = a.Id,
                    AddWhy = a.AddWhy,
                    Answer = a.Answer,
                    AnswerType = a.AnswerType,
                    Name = a.Name,
                    Question = a.Question,
                    Weight = a.Weight,
                    Code = a.Code
                })
                .Where(a => a.Code.ToLower() == code.ToLower())
                .ToListAsync();
            }
        }
    }
}
