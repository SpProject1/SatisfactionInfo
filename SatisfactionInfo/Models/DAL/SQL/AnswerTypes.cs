using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class AnswerTypes
    {
        public AnswerTypes()
        {
            Questions = new HashSet<Questions>();
        }

        public string AnswerType { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }
    }
}
