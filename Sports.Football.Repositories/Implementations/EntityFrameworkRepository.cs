using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sports.Football.Data;
using Sports.Football.Data.Model.Base;

namespace Sports.Football.Repositories.Implementations
{
    public class EntityFrameworkRepository<TModel> : IRepository<TModel>
        where TModel : BaseModel
    {
        protected readonly FootballDbContext DbContext;

        public EntityFrameworkRepository(FootballDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IQueryable<TModel> GetAll()
        {
            return DbContext.Set<TModel>().AsNoTracking();
        }

        public async Task<TModel> GetByIdAsync(int id)
        {
            return await DbContext.Set<TModel>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(TModel model)
        {
            await DbContext.Set<TModel>().AddAsync(model);
            await DbContext.SaveChangesAsync();
        }

        public async Task CreateManyAsync(IEnumerable<TModel> models)
        {
            await DbContext.Set<TModel>().AddRangeAsync(models);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, TModel model)
        {
            DbContext.Set<TModel>().Update(model);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var model = await GetByIdAsync(id);
            DbContext.Set<TModel>().Remove(model);
            await DbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Predicate<TModel> expression)
        {
            return await GetAll().AnyAsync(m => expression.Invoke(m));
        }
    }
}