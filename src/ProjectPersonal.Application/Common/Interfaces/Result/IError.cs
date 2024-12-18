using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Common.Interfaces.Result
{
    internal interface IError
    {
        List<string> Errors { get; set; }

    }
}
