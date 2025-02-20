using Core.Application.Interfaces.Repositories;
using Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();

           
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
       
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual async Task<T> AddAsync(T T)
        {
            await _dbSet.AddAsync(T);
            await _db.SaveChangesAsync();
            return T;
        }
        public virtual async Task<T?> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task DeleteAsync(T T)
        {
            _dbSet.Remove(T);
            await _db.SaveChangesAsync();

        }
        public virtual async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }

    }
}
