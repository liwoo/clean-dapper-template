using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public abstract class DapperBase<TEntity> : IDapperRepository<TEntity>
    {
        private readonly IDbConnection _connection;

        protected DapperBase(IConfiguration configuration)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing")
            {
                using var sqliteConnection =
                    new SqliteConnection(configuration.GetConnectionString("Database"));
                sqliteConnection.Open();
                _connection = sqliteConnection;
            }
            else
            {
                using var conn =
                    new SqlConnection(configuration.GetConnectionString("Database"));
                conn.Open();
                _connection = conn;
            }
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _connection.GetAsync<TEntity>(id);
        }

        public async Task<int?> InsertAsync(TEntity entity)
        {
            return await _connection.InsertAsync(entity);
        }

        public async Task<int> RecordCountAsync()
        {
            return await _connection.RecordCountAsync<TEntity>();
        }
    }
}