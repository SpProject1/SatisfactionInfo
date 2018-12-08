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
        private readonly IVUserQuestionnarieRepo vUserQuestionnarieRepo;

        public UserQuestionnariesRepo(SatisfactionInfoContext db, IVUserQuestionnarieRepo vUserQuestionnarieRepo)
        {
            this.db = db;
            this.vUserQuestionnarieRepo = vUserQuestionnarieRepo;
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
                QuestionNumber = a.QuestionNumber,
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
        public async Task<string> AddQuestionnarieAsync(List<AnsweredDTO> answers)
        {
            try
            {
                var userQuestionnarie = await vUserQuestionnarieRepo.Get(answers.First().Code);
                //usuwanie nie potrzebnego
                var toRemove = answers.Where(a => !userQuestionnarie.Questions.Any(q => q.QuestionNumber.ToString() == a.QuestionNumber)).ToList();
                toRemove.ForEach(a => answers.Remove(a));

                userQuestionnarie.UserQuestionnarie.Code = userQuestionnarie.Code;
                userQuestionnarie.UserQuestionnarie.Name = userQuestionnarie.Name;
                userQuestionnarie.Questions.ToList().ForEach(q =>
                {
                    var item = answers.Where(a => a.QuestionNumber == q.QuestionNumber.ToString()).FirstOrDefault();
                    userQuestionnarie.UserQuestionnarie.UserQuestionnarieAnswersDTOs.Add(new UserQuestionnarieAnswersDTO
                    {
                        Code = userQuestionnarie.Code,
                        Question = q.Question,
                        QuestionNumber = q.QuestionNumber,
                        AddWhy = q.AddWhy,
                        AddWhyName = q.AddWhyName,
                        AnswerType = q.AnswerType,
                        AvailableAnswers = q.AvailableAnswers,
                        Answered = item.Answered,
                        AddWhyBody = item.AddWhyBody                        
                    });                           
                });
                await Add(userQuestionnarie.UserQuestionnarie);
                return "success";
            }
            catch (Exception exe)
            {
                return exe.Message;
            }

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
                        QuestionNumber = a.QuestionNumber,
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
        public async Task<UserQuestionnariesDTO> Get(string code)
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
                        QuestionNumber = a.QuestionNumber,
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
                UserQuestionnarieAnswersDTOs = db.UserQuestionnarieAnswers.Select(a => new UserQuestionnarieAnswersDTO
                {
                    Id = a.Id,
                    Code = a.Code,
                    UserQuestionnarieId = a.UserQuestionnarieId,
                    QuestionNumber = a.QuestionNumber,
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
