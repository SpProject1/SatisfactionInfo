using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DTO
{
    public partial class QuestionsDTO
    {
        public QuestionsDTO()
        {
            QuestionariesQuestionDTO = new HashSet<QuestionariesQuestionDTO>();
            QuestionsAnswerDTO = new HashSet<QuestionsAnswerDTO>();
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public bool? AddWhy { get; set; }

        public virtual ICollection<QuestionariesQuestionDTO> QuestionariesQuestionDTO { get; set; }
        public virtual ICollection<QuestionsAnswerDTO> QuestionsAnswerDTO { get; set; }
    }
}
