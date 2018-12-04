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
        Task<List<UserQuestionnariesDTO>> GetList(string code);
        Task<UserQuestionnariesDTO> Get(int id);       
        Task Add(UserQuestionnariesDTO item);
    }
}
