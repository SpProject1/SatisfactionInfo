using SatisfactionInfo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagHelpers;

namespace SatisfactionInfo.Models.Repo.Interfaces
{
    public interface IUserQuestionnariesRepo
    {
        PageInfo PageInfo { get; set; }
        Task<List<UserQuestionnariesDTO>> GetList(int page, string code = null, string name = null, DateTime? date = null, string description = null);
        Task<UserQuestionnariesDTO> Get(string code);
        Task<UserQuestionnariesDTO> Get(int id);       
        Task Add(UserQuestionnariesDTO item);
        Task<string> AddQuestionnarieAsync(List<AnsweredDTO> answers);
        Task<int> GetQuestionnariesCount(string code);
        Task<FullQuestionnarieDTO> GetFull(string code);
    }
}
