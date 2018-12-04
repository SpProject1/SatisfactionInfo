using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class QuestionnariesQuestion
    {
        public int QuestionId { get; set; }
        public int QuestionnarieId { get; set; }

        public virtual Questions Question { get; set; }
        public virtual Questionnaries Questionnarie { get; set; }
    }
}
