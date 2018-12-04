using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DTO
{
    public partial class QuestionnariesQuestionDTO
    {
        public int QuestionId { get; set; }
        public int QuestionnarieId { get; set; }

        public virtual QuestionsDTO QuestionDTO { get; set; }
        public virtual QuestionnariesDTO QuestionnarieDTO { get; set; }
    }
}
