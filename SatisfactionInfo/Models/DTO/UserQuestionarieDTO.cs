using System;
using System.Collections.Generic;
using System.Linq;

namespace SatisfactionInfo.Models.DTO
{
    public partial class UserQuestionarieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public List<QuestionsDTO> Questions { get; set; }
    }
}
