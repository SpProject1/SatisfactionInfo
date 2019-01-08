using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DTO
{
    public partial class QuestionsDTO
    {
        public QuestionsDTO()
        {
            QuestionnariesQuestionDTO = new HashSet<QuestionnariesQuestionDTO>();
            QuestionsAnswerDTO = new HashSet<QuestionsAnswerDTO>();
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public string AnswerType { get; set; }
        public bool? AddWhy { get; set; }        
        public string AddWhyName { get; set; }
        public int QuestionNumber { get; set; }
        public string AvailableAnswers { get; set; }
        public virtual AnswerTypesDTO AnswerTypeNavigation { get; set; }

        public virtual ICollection<QuestionnariesQuestionDTO> QuestionnariesQuestionDTO { get; set; }
        public virtual ICollection<QuestionsAnswerDTO> QuestionsAnswerDTO { get; set; }
        public List<AnswersDTO> AnswersDTOs { get; set; }
    }
}
