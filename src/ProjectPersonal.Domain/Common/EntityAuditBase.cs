using ProjectPersonal.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Domain.Common
{
    public class EntityAuditBase : IAuditBase, IEntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
