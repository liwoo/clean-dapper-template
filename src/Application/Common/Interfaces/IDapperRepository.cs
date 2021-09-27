using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDapperRepository<TEntity>
    {
        public Task<TEntity> GetAsync(int entityId);

        public Task<IEnumerable<TEntity>> GetAllAsync(
            int pageNumber,
            int rowsPerPage,
            string conditions,
            string orderby,
            object parameters = null
        );

        public Task DeleteByIdAsync(int id);
        public Task<int?> UpdateAsync(TEntity entity);
        public Task<int?> InsertAsync(TEntity entity);
        public Task<int> RecordCountAsync();
    }
}