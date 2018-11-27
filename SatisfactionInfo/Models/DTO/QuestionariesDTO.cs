using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DTO
{
    public partial class QuestionariesDTO
    {
        public QuestionariesDTO()
        {
            QuestionariesQuestionDTO = new HashSet<QuestionariesQuestionDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<QuestionariesQuestionDTO> QuestionariesQuestionDTO { get; set; }
    }
}
