using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactionInfo.Helpers
{
    public static class GuidHelper
    {
        public static string GetShortGuid => Guid.NewGuid().ToString("D").Substring(0, 5);        
    }
}
