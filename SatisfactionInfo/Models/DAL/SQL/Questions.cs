using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class Questions
    {
        public Questions()
        {
            QuestionnariesQuestion = new HashSet<QuestionnariesQuestion>();
            QuestionsAnswer = new HashSet<QuestionsAnswer>();
        }

        public int Id { get; set; }
        public string Question { get; set; }
        public string AnswerType { get; set; }
        public string AddWhyName { get; set; }
        public bool? AddWhy { get; set; }

        public virtual ICollection<QuestionnariesQuestion> QuestionnariesQuestion { get; set; }
        public virtual ICollection<QuestionsAnswer> QuestionsAnswer { get; set; }
    }
}
