using SatisfactionInfo.Models.DAL.SQL;
using SatisfactionInfo.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.Repo.Interfaces
{
    public interface IVUserQuestionarieRepo
    {
        Task<List<VUserQuestionarieDTO>> GetList();
        Task<List<VUserQuestionarieDTO>> GetList(string code);
    }
}
