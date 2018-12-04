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
    public class UserQuestionnariesRepo : IUserQuestionnariesRepo
    {
        private readonly SatisfactionInfoContext db;

        public UserQuestionnariesRepo(SatisfactionInfoContext db)
        {
            this.db = db;
        }
        public async Task Add(UserQuestionnariesDTO item)
        {
            var dbItem = new UserQuestionnaries
            {
                Name = item.Name,
                Code = item.Code,
                Date = DateTime.Now
            };
            await db.UserQuestionnaries.AddAsync(dbItem);
            await db.SaveChangesAsync();
            await AddQuestions(dbItem.Id, item.UserQuestionnarieAnswersDTOs.ToList());
        }
        private async Task AddQuestions(int userQuestionnarieId, List<UserQuestionnarieAnswersDTO> userQuestionnarieAnswersDTOs)
        {
            var answers = new List<UserQuestionnarieAnswers>();
            userQuestionnarieAnswersDTOs.ForEach(a => answers.Add(new UserQuestionnarieAnswers
            {
                Code = a.Code,
                UserQuestionnarieId = userQuestionnarieId,
                QuestionNomber = a.QuestionNomber,
                Question = a.Question,
                AvailableAnswers = a.AvailableAnswers,
                AnswerType = a.AnswerType,
                Answered = a.Answered,
                AddWhy = a.AddWhy,
                AddWhyBody = a.AddWhyBody,
                AddWhyName = a.AddWhyName
            }));
            await db.UserQuestionnarieAnswers.AddRangeAsync(answers);
            await db.SaveChangesAsync();
        }

        public async Task<UserQuestionnariesDTO> Get(int id)
        {
            var result = await db.UserQuestionnaries
                .Where(a => a.Id == id)
                .Select(b => new UserQuestionnariesDTO
            {
                Code = b.Code,
                Date = b.Date,
                Id = b.Id,
                Name = b.Name,
                UserQuestionnarieAnswersDTOs = db.UserQuestionnarieAnswers.Select(a => new UserQuestionnarieAnswersDTO
                {
                    Id = a.Id,
                    Code = a.Code,
                    UserQuestionnarieId = a.UserQuestionnarieId,
                    QuestionNomber = a.QuestionNomber,
                    Question = a.Question,
                    AvailableAnswers = a.AvailableAnswers,
                    AnswerType = a.AnswerType,
                    Answered = a.Answered,
                    AddWhy = a.AddWhy,
                    AddWhyBody = a.AddWhyBody,
                    AddWhyName = a.AddWhyName
                }).Where(c => c.UserQuestionnarieId == b.Id).ToList()
            }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<UserQuestionnariesDTO>> GetList()
        {
            var result = await db.UserQuestionnaries.Select(b => new UserQuestionnariesDTO
            {
                Code = b.Code,
                Date = b.Date,
                Id = b.Id,
                Name = b.Name,
                UserQuestionnarieAnswersDTOs = db.UserQuestionnarieAnswers.Select(a => new UserQuestionnarieAnswersDTO {
                    Id= a.Id,
                    Code = a.Code,
                    UserQuestionnarieId = a.UserQuestionnarieId,
                    QuestionNomber = a.QuestionNomber,
                    Question = a.Question,
                    AvailableAnswers = a.AvailableAnswers,
                    AnswerType = a.AnswerType,
                    Answered = a.Answered,
                    AddWhy = a.AddWhy,
                    AddWhyBody = a.AddWhyBody,
                    AddWhyName = a.AddWhyName
                }).Where(c => c.UserQuestionnarieId == b.Id).ToList()
            }).ToListAsync();
            return result;
        }
        public async Task<List<UserQuestionnariesDTO>> GetList(string code)
        {
            var result = await db.UserQuestionnaries
                .Where(a => a.Code == code)
                .Select(b => new UserQuestionnariesDTO
            {
                Code = b.Code,
                Date = b.Date,
                Id = b.Id,
                Name = b.Name,
                UserQuestionnarieAnswersDTOs = db.UserQuestionnarieAnswers.Select(a => new UserQuestionnarieAnswersDTO
                {
                    Id = a.Id,
                    Code = a.Code,
                    UserQuestionnarieId = a.UserQuestionnarieId,
                    QuestionNomber = a.QuestionNomber,
                    Question = a.Question,
                    AvailableAnswers = a.AvailableAnswers,
                    AnswerType = a.AnswerType,
                    Answered = a.Answered,
                    AddWhy = a.AddWhy,
                    AddWhyBody = a.AddWhyBody,
                    AddWhyName = a.AddWhyName
                }).Where(c => c.UserQuestionnarieId == b.Id).ToList()
            }).ToListAsync();
            return result;
        }
    }
}
