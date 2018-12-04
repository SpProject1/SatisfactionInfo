using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.DTO
{
    public class UserQuestionnariesDTO
    {
        public UserQuestionnariesDTO()
        {
            UserQuestionnarieAnswersDTOs = new HashSet<UserQuestionnarieAnswersDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Code { get; set; }

        public virtual ICollection<UserQuestionnarieAnswersDTO> UserQuestionnarieAnswersDTOs { get; set; }
    }
}
