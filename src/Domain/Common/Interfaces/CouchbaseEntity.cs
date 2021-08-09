using Newtonsoft.Json;

using Domain.Common.Extensions;

namespace Domain.Common.Interfaces
{
    public abstract class CouchbaseEntity<TEntity> where TEntity : class
    {
        protected CouchbaseEntity()
        {
            Entity = typeof(TEntity).GetEntityName();
        }

        public string Id { get; set; }

        [JsonProperty]
        public string Entity { get; protected set; }
    }
}