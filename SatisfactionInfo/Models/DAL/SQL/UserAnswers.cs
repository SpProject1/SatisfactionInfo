using System;
using System.Collections.Generic;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class UserAnswers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
    }
}
