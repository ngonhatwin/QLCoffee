using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Dtos.Request
{
    public class AuthenRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
