using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sports.Football.Data.Model.Base;

namespace Sports.Football.Data
{
    public interface IRepository<TModel> where TModel : Entity
    {
        IQueryable<TModel> GetAll();

        Task<TModel> GetByIdAsync(int id);

        Task CreateAsync(TModel model);

        Task CreateManyAsync(IEnumerable<TModel> models);

        Task UpdateAsync(int id, TModel model);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(Predicate<TModel> expression);
    }
}