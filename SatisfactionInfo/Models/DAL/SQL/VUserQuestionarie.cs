using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class VUserQuestionarie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Question { get; set; }
        public bool? AddWhy { get; set; }
        public string Answer { get; set; }
        public decimal Weight { get; set; }
        public string AnswerType { get; set; }
        public string Code { get; set; }

    }
}
