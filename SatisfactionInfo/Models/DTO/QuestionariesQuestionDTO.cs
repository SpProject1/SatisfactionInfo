using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DTO
{
    public partial class QuestionariesQuestionDTO
    {
        public int QuestionId { get; set; }
        public int QuestionarieId { get; set; }

        public virtual QuestionsDTO QuestionDTO { get; set; }
        public virtual QuestionariesDTO QuestionarieDTO { get; set; }
    }
}
