using SatisfactionInfo.Models.DAL.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.DTO
{
    public class QuestionAnswerViewModel
    {
        public  List<QuestionsAnswerDTO> List { get; set; }
        public int QuestionId { get; set; }
    }
}
