using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Domain.Common.Interfaces
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }
}
