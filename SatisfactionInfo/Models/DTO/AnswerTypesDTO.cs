using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.DTO
{
    public partial class AnswerTypesDTO
    {
        public AnswerTypesDTO()
        {
            Questions = new HashSet<QuestionsDTO>();
        }

        public string AnswerType { get; set; }

        public virtual ICollection<QuestionsDTO> Questions { get; set; }
    }
}
