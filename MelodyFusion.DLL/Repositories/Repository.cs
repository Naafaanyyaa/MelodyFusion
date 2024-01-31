using System.Linq.Expressions;
using MelodyFusion.DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using PetHospital.Data.Entities.Abstract;

namespace MelodyFusion.DLL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _context.Set<TEntity>().ToListAsync();
        }

        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string? includeString = null,
            bool disableTracking = true)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            if (!string.IsNullOrEmpty(includeString))
                query = query.Include(includeString);

            if (disableTracking)
                query = query.AsNoTracking();

            if (orderBy != null)
                query = orderBy(query);

            return query.ToListAsync();
        }

        public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            List<Expression<Func<TEntity, object>>>? includes = null, bool disableTracking = true)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);


            if (disableTracking)
                query = query.AsNoTracking();

            if (orderBy != null)
                query = orderBy(query);

            return query.ToListAsync();
        }

        public virtual Task<TEntity?> GetByIdAsync(string id)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(i => i.Id == id);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteById(string id)
        {
            var animal = _context.Set<TEntity>().Single(x => x.Id == id);
            _context.Remove(animal);
            await _context.SaveChangesAsync();
        }
    }
}
