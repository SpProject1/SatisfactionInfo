using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class UserQuestionnaries
    {
        public UserQuestionnaries()
        {
            UserQuestionnarieAnswers = new HashSet<UserQuestionnarieAnswers>();
        }

        public int Id { get; set; }        
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Code { get; set; }

        public virtual ICollection<UserQuestionnarieAnswers> UserQuestionnarieAnswers { get; set; }
    }
}
