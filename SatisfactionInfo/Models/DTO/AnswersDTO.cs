using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SatisfactionInfo.Models.DTO
{
    public partial class AnswersDTO
    {
        public AnswersDTO()
        {
            QuestionsAnswerDTO = new HashSet<QuestionsAnswerDTO>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="To pole jest wymagane")]       
        public string Answer { get; set; }
        public decimal? Weight { get; set; }    

        public virtual ICollection<QuestionsAnswerDTO> QuestionsAnswerDTO { get; set; }
    }
}
