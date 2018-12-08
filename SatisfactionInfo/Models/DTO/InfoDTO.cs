using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Models.DTO
{
    public class InfoDTO
    {
        public InfoDTO() {}
        public InfoDTO(InfoType type, string message)
        {
            Type = type;
            Message = message;
        }
        public enum InfoType
        {
            Info = 1,
            Success = 2,
            Error = 3
        }
        public InfoType Type { get; set; }
        public string Message { get; set; }
    }
}
