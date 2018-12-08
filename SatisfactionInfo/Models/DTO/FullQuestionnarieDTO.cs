using System;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactionInfo.Models.DTO
{
    public partial class FullQuestionnarieDTO
    {
        public FullQuestionnarieDTO()
        {
            UserQuestionnarie = new UserQuestionnariesDTO();           
        }
        public int Id { get; set; }
        public string Name { get; set; }  
        public string Code { get; set; }
        public string Key { get; set; }
        public List<QuestionsDTO> Questions { get; set; }
        public UserQuestionnariesDTO UserQuestionnarie { get; set; }
    }
}
