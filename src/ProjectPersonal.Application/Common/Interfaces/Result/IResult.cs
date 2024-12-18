using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Common.Interfaces.Result
{
    public interface IResult<T>
    {
        int Code { get; set; } 
        string Message { get; set; }
        T Data { get; set; }
      
       
    }
}
