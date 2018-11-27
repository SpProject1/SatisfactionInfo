using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class QuestionsAnswer
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }

        public virtual Answers Answer { get; set; }
        public virtual Questions Question { get; set; }
    }
}
