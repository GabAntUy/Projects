using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class ApiException
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    
    }
}
