using SatisfactionInfo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.Repo.Interfaces
{
    public interface IUserQuestionnariesRepo
    {
        Task<List<UserQuestionnariesDTO>> GetList();
        Task<UserQuestionnariesDTO> Get(string code);
        Task<UserQuestionnariesDTO> Get(int id);       
        Task Add(UserQuestionnariesDTO item);
        Task<string> AddQuestionnarieAsync(List<AnsweredDTO> answers);
        Task<int> GetQuestionnariesCount(string code);
        Task<FullQuestionnarieDTO> GetFull(string code);
    }
}
