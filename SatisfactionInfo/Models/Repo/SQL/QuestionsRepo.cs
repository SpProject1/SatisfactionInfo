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
    public class QuestionsRepo : IQuestionsRepo
    {
        private readonly SatisfactionInfoContext db;

        public QuestionsRepo(SatisfactionInfoContext db)
        {
            this.db = db;
        }
        public async Task<List<QuestionsDTO>> GetList()
        {
            List<QuestionsDTO> result = new List<QuestionsDTO>();
            var list = await db.Questions.OrderByDescending(a => a.Id)
                .Include(q => q.AnswerTypeNavigation)
                .Include(qa => qa.QuestionsAnswer)
                .ThenInclude(a => a.Answer)
                .ToListAsync();
            list.ForEach(a =>
            {
                var qaList = new List<QuestionsAnswerDTO>();
                a.QuestionsAnswer.ToList().ForEach(b =>
                {
                    qaList.Add(new QuestionsAnswerDTO
                    {
                        AnswerId = b.AnswerId,
                        QuestionId = b.QuestionId,
                        AnswerDTO = new AnswersDTO
                        {
                            Answer = b.Answer.Answer,
                            Id = b.Answer.Id
                        }
                    });
                });
                result.Add(new QuestionsDTO
                {
                    Id = a.Id,
                    Question = a.Question,
                    AddWhy = a.AddWhy,
                    AddWhyName = a.AddWhyName,
                    AnswerType = a.AnswerType,
                    AnswerTypeNavigation = new AnswerTypesDTO { AnswerType = a.AnswerTypeNavigation.AnswerType },
                    QuestionsAnswerDTO = qaList
                });
            });
            return result;
        }

        public async Task Add(QuestionsDTO item)
        {
            var question = new Questions
            {
                Id = item.Id,
                Question = item.Question,
                AnswerType = item.AnswerType,
                AddWhyName = item.AddWhyName,
                AddWhy = item.AddWhy
            };
            db.Questions.Add(question);
            await db.SaveChangesAsync();
        }

        public async Task Update(QuestionsDTO item)
        {
            var question = await db.Questions.FindAsync(item.Id);
            if (question != null)
            {
                question.Question = item.Question;
                question.AnswerType = item.AnswerType;
                question.AddWhyName = item.AddWhyName;
                question.AddWhy = item.AddWhy;
                db.Entry(question).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
        public async Task Delete(int? id)
        {
            var question = id != null ? await db.Questions.FindAsync(id) : null;
            if (question != null)
            {
                db.Questions.Remove(question);
                await db.SaveChangesAsync();
            }
        }

        public List<AnswersDTO> GetAnswersList()
        {
            return db.Answers.Select(answer => new AnswersDTO
            {
                Id = answer.Id,
                Answer = answer.Answer
            }).OrderByDescending(a => a.Id).ToList();
        }
        public List<AnswerTypesDTO> GetAnswerTypesList()
        {
            return db.AnswerTypes.Select(answer => new AnswerTypesDTO
            {
                AnswerType = answer.AnswerType
            }).ToList();
        }

        public async Task<List<QuestionsAnswerDTO>> GetListQuestionsAnswer(int questionId)
        {
            List<QuestionsAnswerDTO> result = new List<QuestionsAnswerDTO>();
            var list = await db.QuestionsAnswer
                .Where(a => a.QuestionId == questionId)
                .Include(a => a.Answer)
                .ToListAsync();
            list.ForEach(a =>
            {
                result.Add(new QuestionsAnswerDTO
                {
                    QuestionId = a.QuestionId,
                    AnswerId = a.AnswerId,
                    AnswerDTO = new AnswersDTO
                    {
                        Id = a.AnswerId,
                        Answer = a.Answer.Answer
                    }
                });
            });
            return result;
        }
        public async Task AddQuestionAnswer(QuestionsAnswerDTO item)
        {
            var questionsAnswer = new QuestionsAnswer
            {
                QuestionId = item.QuestionId,
                AnswerId = item.AnswerId
            };
            db.QuestionsAnswer.Add(questionsAnswer);
            await db.SaveChangesAsync();
        }

        public async Task DeleteQuestionAnswer(QuestionsAnswerDTO item)
        {
            var questionAnswer = item != null ? await db.QuestionsAnswer.Where(a => a.QuestionId == item.QuestionId && a.AnswerId == item.AnswerId).FirstOrDefaultAsync() : null;
            if (questionAnswer != null)
            {
                db.Entry(questionAnswer).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                await db.SaveChangesAsync();
            }
        }
        public bool QuestionsAnswerExists(int questionId, int? answerId = null)
        {
            if (answerId == null)
                return db.QuestionsAnswer.Any(a => a.QuestionId == questionId);
            return db.QuestionsAnswer.Any(a => a.QuestionId == questionId && a.AnswerId == answerId);
        }
    }
}
