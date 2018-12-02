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
        public UserQuestionarieDTO Get(string code)
        {
            using (SatisfactionInfoContext db = new SatisfactionInfoContext())
            {
                var questionaries = db.VUserQuestionarie.Where(a => a.Code.ToLower() == code.ToLower()).ToList();
                if (questionaries != null && questionaries.Count != 0)
                {
                    var result = new UserQuestionarieDTO();
                    var questionsStrings = questionaries.Select(a => new { a.Question, a.AddWhy, a.AnswerType }).Distinct().ToList();
                    result.Id = questionaries.First().Id;
                    result.Name = questionaries.First().Name;
                    result.Questions = questionsStrings.Select(a => new QuestionsDTO
                    {
                        Question = a.Question,
                        AddWhy = a.AddWhy,
                        AnswerType = a.AnswerType
                    }).ToList();
                    result.Questions.ForEach(q =>
                    {
                        q.AnswersDTOs = questionaries.Where(b => b.Question == q.Question).Select(c => new AnswersDTO
                        {
                            Answer = c.Answer                          
                        }).ToList();
                    });

                    return result;
                }
                return null;
            }
        }
    }
}
