using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class Questionaries
    {
        public Questionaries()
        {
            QuestionariesQuestion = new HashSet<QuestionariesQuestion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<QuestionariesQuestion> QuestionariesQuestion { get; set; }
    }
}
