using ProjectPersonal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Domain.Entities
{
    public class Cart : EntityAuditBase
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; } = new Product();
    }
}
