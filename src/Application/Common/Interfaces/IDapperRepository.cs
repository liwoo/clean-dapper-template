using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDapperRepository<TEntity>
    {
        public Task<TEntity> GetAsync(int id);
        public Task<int> RecordCountAsync();
        public Task<int?> InsertAsync(TEntity entity);
    }
}