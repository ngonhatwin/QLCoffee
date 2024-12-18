using ProjectPersonal.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Domain.Common
{
    public class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }
    }
}
