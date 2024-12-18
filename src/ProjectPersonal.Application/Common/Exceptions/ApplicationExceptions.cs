using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Common.Exceptions
{
    public class ApplicationExceptions : ApplicationException
    {
        public int Code { get; set; }
        public string Message {  get; set; }
        public ApplicationExceptions() { }

        public ApplicationExceptions(int code, string message) { Code = code; Message = message; }
        
    }
}
