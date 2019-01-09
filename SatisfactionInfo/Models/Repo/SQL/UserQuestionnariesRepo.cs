using Microsoft.EntityFrameworkCore;
using SatisfactionInfo.Models.DAL.SQL;
using SatisfactionInfo.Models.DTO;
using SatisfactionInfo.Models.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagHelpers;

namespace SatisfactionInfo.Models.Repo.SQL
{
    public class UserQuestionnariesRepo : IUserQuestionnariesRepo
    {
        private readonly SatisfactionInfoContext db;
        const int pageSize = 2;
        public PageInfo PageInfo { get; set; }
        public UserQuestionnariesRepo(SatisfactionInfoContext db)
        {
            this.db = db;
            PageInfo = new PageInfo();
        }
        public async Task Add(UserQuestionnariesDTO item)
        {
            var dbItem = new UserQuestionnaries
            {
                Name = item.Name,
                Code = item.Code,
                Date = DateTime.Now,
                Description = item.Description
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
                var userQuestionnarie = await GetFull(answers.First().Code);
                if (userQuestionnarie.Active != true)
                {
                    return "Ta ankieta jest już nieaktywna, skontaktuj sie z administratorem.";
                }
                int questionariesCount = await GetQuestionnariesCount(answers.First().Code);
                if (questionariesCount > userQuestionnarie.MaxAnswers)
                {
                    return "Za duzo odpowiedzi dla aktualej ankiety, skontaktuj sie z administratorem.";
                }
                //usuwanie nie potrzebnego
                var toRemove = answers.Where(a => !userQuestionnarie.Questions.Any(q => q.QuestionNumber.ToString() == a.QuestionNumber)).ToList();
                toRemove.ForEach(a => answers.Remove(a));

                userQuestionnarie.UserQuestionnarie.Code = userQuestionnarie.Code;
                userQuestionnarie.UserQuestionnarie.Name = userQuestionnarie.Name;
                userQuestionnarie.UserQuestionnarie.Description = userQuestionnarie.Description;
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
                    Description = b.Description,
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
                    Description = b.Description,
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

        public async Task<List<UserQuestionnariesDTO>> GetList(int page, string code = null, string name = null, DateTime? date = null, string description = null, int? pageSizeLocal = null)
        {
            page = page < 1 ? 1 : page;
            var toRemove = new List<UserQuestionnariesDTO>();
            var result = db.UserQuestionnaries
            .Select(b => new UserQuestionnariesDTO
            {
                Code = b.Code,
                Date = b.Date,
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
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
            });

            PageInfo = new PageInfo
            {
                CurrentPage = page,
                ItemPerPage = pageSizeLocal ?? pageSize,
                TotalItems = await result.CountAsync()
            };

            if (code != null)
            {
                toRemove.AddRange(result.Where(a => !a.Code.ToLower().Contains(code.ToLower())).ToList());
            }
            if (name != null)
            {
                toRemove.AddRange(result.Where(a => !a.Name.ToLower().Contains(name.ToLower())).ToList());
            }
            if (description != null)
            {
                toRemove.AddRange(result.Where(a => a.Description == null || (a.Description != null && !a.Description.ToLower().Contains(description.ToLower()))).ToList());
            }
            if (date.HasValue)
            {
                toRemove.AddRange(result.Where(a => a.Date.Value.Date != date.Value.Date).ToList());
            }
            var excludeIds = new HashSet<int>(toRemove.Select(a => a.Id));
            if (code == null && name == null && description == null && !date.HasValue)
                return await result
                  .Skip((page - 1) * PageInfo.ItemPerPage).Take(PageInfo.ItemPerPage)
                  .OrderByDescending(a => a.Date)
                  .ToListAsync();

            PageInfo.CurrentPage = 1;
            PageInfo.ItemPerPage = int.MaxValue;
            return await result
                    .Where(a => !excludeIds.Contains(a.Id))
                    .OrderByDescending(a => a.Date)
                    .ToListAsync();
        }

        public async Task<int> GetQuestionnariesCount(string code)
        {
            return await db.UserQuestionnaries.Where(a => a.Code == code).Select(a => a.Code).CountAsync();
        }
        public async Task<FullQuestionnarieDTO> GetFull(string code)
        {
            var questionnarie = await db.Questionnaries.Where(a => a.Code.ToLower() == code.ToLower()).FirstOrDefaultAsync();
            if (questionnarie == null)
            {
                return new FullQuestionnarieDTO { ErrorMessage = $"Nie znaloziono ankiety (kod: {code})" };
            }
            if (questionnarie != null && questionnarie.Active != true)
            {
                return new FullQuestionnarieDTO { ErrorMessage = "Ta ankieta jest już nieaktywna, skontaktuj sie z administratorem." };
            }
            int questionariesCount = await db.UserQuestionnaries.Where(a => a.Code.ToLower() == code.ToLower()).CountAsync();
            if (questionariesCount > questionnarie.MaxAnswers)
            {
                return new FullQuestionnarieDTO { ErrorMessage = $"Za duzo odpowiedzianych ankiet (kod: {code}), skontaktuj sie z administratorem." };
            }
            if (questionnarie != null)
            {
                var questionnariesQuestions = await db.QuestionnariesQuestion.Where(a => a.QuestionnarieId == questionnarie.Id).ToListAsync();

                var result = new FullQuestionnarieDTO();
                result.Id = questionnarie.Id;
                result.Name = questionnarie.Name;
                result.Code = questionnarie.Code;
                result.Active = questionnarie.Active;
                result.MaxAnswers = questionnarie.MaxAnswers;
                result.Description = questionnarie.Description;
                result.Questions = await db.Questions.Where(a => questionnariesQuestions.Any(b => b.QuestionId == a.Id)).Select(a => new QuestionsDTO
                {
                    Id = a.Id,
                    AddWhy = a.AddWhy,
                    AddWhyName = a.AddWhyName,
                    AnswerType = a.AnswerType,
                    Question = a.Question,
                    QuestionNumber = questionnariesQuestions.Where(c => c.QuestionnarieId == questionnarie.Id && c.QuestionId == a.Id).Select(c => c.QuestionNumber).FirstOrDefault()
                }).ToListAsync();

                result.Questions.ForEach(q =>
                {
                    var questionsAnswerDTO = db.QuestionsAnswer.Where(a => a.QuestionId == q.Id).ToList();
                    q.AnswersDTOs = db.Answers.Where(a => questionsAnswerDTO.Any(b => b.AnswerId == a.Id)).Select(c => new AnswersDTO
                    {
                        Id = c.Id,
                        Answer = c.Answer

                    }).ToList();
                    q.AvailableAnswers = String.Join(';', q.AnswersDTOs.Select(a => a.Answer).ToArray());
                });

                return result;
            }
            return null;
        }
    }
}
