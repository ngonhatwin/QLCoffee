using ProjectPersonal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Dtos.Response
{
    public class AuthenResponse
    {
        public string Username { get; set; }
        public string Token {  get; set; }
        public string RefreshTokenHash { get; set; } = null!;
        public Byte Role { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
