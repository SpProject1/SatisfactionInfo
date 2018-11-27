using SatisfactionInfo.Models.DAL.SQL;
using SatisfactionInfo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.Repo.Interfaces
{
    public interface IAnswersRepo
    {
        Task<List<AnswersDTO>> GetList();
        Task<AnswersDTO> Get(int? id);
        Task Add(AnswersDTO item);
        Task Update(AnswersDTO item);
        Task Delete(int? id);
    }
}
