using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.DTO
{
    public class UserQuestionnarieAnswersDTO   
    {
        public int Id { get; set; }
        public string Code { get; set; }      
        public int? UserQuestionnarieId { get; set; }
        public int? QuestionNumber { get; set; }
        public string Question { get; set; }
        public string AvailableAnswers { get; set; }
        public string Answered { get; set; }
        public string AnswerType { get; set; }
        public string AddWhyName { get; set; }
        public bool? AddWhy { get; set; }
        public string AddWhyBody { get; set; }

        public virtual UserQuestionnariesDTO UserQuestionnarieDTO { get; set; }
    }
}
