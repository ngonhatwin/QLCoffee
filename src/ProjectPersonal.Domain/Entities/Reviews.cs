using ProjectPersonal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Domain.Entities
{
    public class Reviews : EntityAuditBase
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
