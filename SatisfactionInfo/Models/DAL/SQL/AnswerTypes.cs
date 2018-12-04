using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class AnswerTypes
    {
        public AnswerTypes()
        {
            Answers = new HashSet<Answers>();
        }
        public string AnswerType { get; set; }

        public virtual ICollection<Answers> Answers { get; set; }
    }
}
