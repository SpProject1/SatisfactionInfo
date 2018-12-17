using SatisfactionInfo.Models.DAL.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.DTO
{
    public class QuestionnarieQuestionsViewModel
    {
        public List<QuestionnariesQuestion> List { get; set; }
        public int QuestionnarieId { get; set; }
    }
}
