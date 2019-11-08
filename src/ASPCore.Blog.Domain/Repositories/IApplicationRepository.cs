using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPCore.Blog.Domain.Repositories
{
    public interface IApplicationRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        TEntity GetByID(int entityId);
        TEntity GetByID(int firstId, int secondId);
        void Insert(TEntity entity);
        Task Delete(int entityId);
        Task Delete(int firstId, int secondId);
        void Update(TEntity entity);
    }
}
