using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IHrmRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task Save();
    }
}
