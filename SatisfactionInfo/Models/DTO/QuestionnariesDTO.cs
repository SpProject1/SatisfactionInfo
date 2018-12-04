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

        public virtual ICollection<QuestionnariesQuestionDTO> QuestionnariesQuestionDTO { get; set; }
    }
}
