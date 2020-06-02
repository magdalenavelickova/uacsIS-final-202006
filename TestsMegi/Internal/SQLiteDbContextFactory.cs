using System;
using System.Data.Common;
using BankApplication.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace UniversityApplication.Tests.Internal
{
    public class SQLiteDbContextFactory
        : IDisposable
    {
        private DbConnection _connection;

        private DbContextOptions<BankDataContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<BankDataContext>()
                .UseSqlite(_connection)
                .Options;
        }

        public BankDataContext CreateContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                var options = CreateOptions();
                using var context = new BankDataContext(options);
                context.Database.EnsureCreated();
            }

            return new BankDataContext(CreateOptions());
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
