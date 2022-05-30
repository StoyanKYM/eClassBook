using Microsoft.EntityFrameworkCore;

namespace eClassBook.Data.Repositories
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity>
        where TEntity : class
    {
        private readonly eClassBookDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GeneralRepository(eClassBookDbContext schoolBookContext)
        {
            this._context = schoolBookContext;
            this._dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Query()
        {
            return this._dbSet;
        }

        public TEntity GetById(object id)
        {
            return this._dbSet.Find(id);
        }

        public List<TEntity> GetWithoutTracking()
        {
            return this._dbSet.AsNoTracking().ToList();
        }

        public void Create(TEntity entity)
        {
            this._dbSet.Add(entity);
            this.SaveChanges();
        }

        public void CreateWithoutSaving(TEntity entity)
        {
            this._dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            this._dbSet.Update(entity);
            this.SaveChanges();
        }

        public void UpdateWithoutSaving(TEntity entity)
        {
            this._dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            this._dbSet.Remove(entity);
        }

        public int SaveChanges()
        {
            return this._context.SaveChanges();
        }
    }
}
