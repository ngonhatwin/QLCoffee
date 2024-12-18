using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Common.Interfaces.Repository
{
    public interface IUnitofwork<T> : IDisposable where T : class
    {
        DbSet<T> Entities { get; }
        IRepositoryBaseAsync<T, K> GetRepository<T, K>() where T : class;
    }
}
