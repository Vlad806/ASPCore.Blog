using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPCore.Blog.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPCore.Blog.Domain.Repositories
{
    public class ApplicationRepository<TEntity> : IApplicationRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<TEntity> _entities;

        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> Get()
        {
            return Entities;
        }

        public TEntity GetByID(int entityId)
        {
            return Entities.Find(entityId);
        }

        public TEntity GetByID(int firstId, int secondId)
        {
            return Entities.Find(firstId, secondId);
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Add(entity);
            _context.SaveChanges();
        }

        public async Task Delete(int entityId)
        {
            var entity = GetByID(entityId);
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int firstId, int secondId)
        {
            var entity = GetByID(firstId, secondId);
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        protected DbSet<TEntity> Entities
        {
            get { return _entities ?? (_entities = _context.Set<TEntity>()); }
        }
    }
}
