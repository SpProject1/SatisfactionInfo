using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DTO
{
    public partial class VUserQuestionarieDTO
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
