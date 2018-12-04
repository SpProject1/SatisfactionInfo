using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class UserQuestionnarieAnswers
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int? UserQuestionnarieId { get; set; }
        public int? QuestionNomber { get; set; }
        public string Question { get; set; }
        public string AvailableAnswers { get; set; }
        public string Answered { get; set; }
        public string AnswerType { get; set; }
        public string AddWhyName { get; set; }
        public bool? AddWhy { get; set; }
        public string AddWhyBody { get; set; }

        public virtual UserQuestionnaries UserQuestionnarie { get; set; }
    }
}
