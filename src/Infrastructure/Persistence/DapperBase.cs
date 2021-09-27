using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Persistence
{
    public abstract class DapperBase<TEntity> : IDapperRepository<TEntity>
    {
        private readonly string _connectionString;

        protected IDbConnection CreateConnection()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IDbConnection connection = (env == "Testing") ?
                new SqliteConnection(_connectionString) :
                new NpgsqlConnection(_connectionString);

            connection.Open();
            return connection;
        }

        protected DapperBase(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        public async Task<TEntity> GetAsync(int entityId)
        {
            using IDbConnection connection = CreateConnection();
            return await connection.GetAsync<TEntity>(entityId);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
            int pageNumber,
            int rowsPerPage,
            string conditions,
            string orderby,
            object parameters = null
        )
        {
            using IDbConnection connection = CreateConnection();
            return await connection.GetListPagedAsync<TEntity>(
                pageNumber,
                rowsPerPage,
                conditions,
                orderby,
                parameters
            );
        }

        public async Task<int?> InsertAsync(TEntity entity)
        {
            using IDbConnection connection = CreateConnection();
            return await connection.InsertAsync(entity);
        }

        public async Task<int> RecordCountAsync()
        {
            using IDbConnection connection = CreateConnection();
            return await connection.RecordCountAsync<TEntity>();
        }

        public async Task DeleteByIdAsync(int id)
        {
            using IDbConnection connection = CreateConnection();
            _ = await connection.DeleteAsync<TEntity>(id);
        }

        public async Task<int?> UpdateAsync(TEntity entity)
        {
            using IDbConnection connection = CreateConnection();
            return await connection.UpdateAsync(entity);
        }
    }
}