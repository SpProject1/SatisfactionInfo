using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DTO
{
    public partial class QuestionnariesDTO
    {
        public QuestionnariesDTO()
        {
            QuestionnariesQuestionDTO = new HashSet<QuestionnariesQuestionDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public int MaxAnswers { get; set; }
        public string Description { get; set; }

        public virtual ICollection<QuestionnariesQuestionDTO> QuestionnariesQuestionDTO { get; set; }
    }
}
