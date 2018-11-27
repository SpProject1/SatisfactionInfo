using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DTO
{
    public partial class QuestionsAnswerDTO
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }

        public virtual AnswersDTO AnswerDTO { get; set; }
        public virtual QuestionsDTO QuestionDTO { get; set; }
    }
}
