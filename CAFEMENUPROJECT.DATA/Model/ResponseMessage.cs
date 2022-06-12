using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.DATA.Model
{
    public class ResponseMessage
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public object Data { get; set; }
    }
}
