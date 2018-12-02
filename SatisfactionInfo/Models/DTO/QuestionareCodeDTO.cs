using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.DTO
{
    public class QuestionareCodeDTO
    {
        [Required]
        public string Code { get; set; }
    }
}
