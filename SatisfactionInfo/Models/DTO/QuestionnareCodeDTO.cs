using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.DTO
{
    public class QuestionnareCodeDTO
    {
        [Required]
        public string Code { get; set; }
    }
}
