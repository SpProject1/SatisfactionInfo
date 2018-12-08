using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.DTO
{
    public class AnsweredDTO
    {
        public string Code { get; set; }
        public string QuestionNumber { get; set; }
        public string Answered { get; set; }
        public string AddWhyBody { get; set; }
    }
}
