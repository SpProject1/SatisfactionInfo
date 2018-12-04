using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class Answers
    {
        public Answers()
        {
            QuestionsAnswer = new HashSet<QuestionsAnswer>();
        }

        public int Id { get; set; }
        public string Answer { get; set; }
        public decimal? Weight { get; set; }      

        public virtual AnswerTypes AnswerTypeNavigation { get; set; }
        public virtual ICollection<QuestionsAnswer> QuestionsAnswer { get; set; }
    }
}
