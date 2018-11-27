using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class QuestionariesQuestion
    {
        public int QuestionId { get; set; }
        public int QuestionarieId { get; set; }

        public virtual Questions Question { get; set; }
        public virtual Questionaries Questionarie { get; set; }
    }
}
