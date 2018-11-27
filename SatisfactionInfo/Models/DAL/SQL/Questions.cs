using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class Questions
    {
        public Questions()
        {
            QuestionariesQuestion = new HashSet<QuestionariesQuestion>();
            QuestionsAnswer = new HashSet<QuestionsAnswer>();
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public bool? AddWhy { get; set; }

        public virtual ICollection<QuestionariesQuestion> QuestionariesQuestion { get; set; }
        public virtual ICollection<QuestionsAnswer> QuestionsAnswer { get; set; }
    }
}
