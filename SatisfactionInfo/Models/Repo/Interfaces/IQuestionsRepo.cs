using SatisfactionInfo.Models.DAL.SQL;
using SatisfactionInfo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.Repo.Interfaces
{
    public interface IQuestionsRepo
    {
        Task<List<QuestionsDTO>> GetList();
        Task<List<QuestionsAnswerDTO>> GetListQuestionsAnswer(int questionId);
        Task Add(QuestionsDTO item);
        Task AddQuestionAnswer(QuestionsAnswerDTO item);
        Task Update(QuestionsDTO item);
        Task Delete(int? id);
        Task DeleteQuestionAnswer(QuestionsAnswerDTO item);
        List<AnswersDTO> GetAnswersList();
        List<AnswerTypesDTO> GetAnswerTypesList();
        bool QuestionsAnswerExists(int questionId, int? answerId = null);
    }
}
