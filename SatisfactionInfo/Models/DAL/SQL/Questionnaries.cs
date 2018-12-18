using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class Questionnaries
    {
        public Questionnaries()
        {
            QuestionnariesQuestion = new HashSet<QuestionnariesQuestion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public int MaxAnswers { get; set; }
        public string Description { get; set; }

        public virtual ICollection<QuestionnariesQuestion> QuestionnariesQuestion { get; set; }
    }
}
