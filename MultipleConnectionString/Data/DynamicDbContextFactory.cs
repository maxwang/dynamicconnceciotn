using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace MultipleConnectionString.Data
{
    record ConnectionConfiguration(string ConnectionString);
    class DynamicDbContextFactory<TContext> where TContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        private Func<IServiceProvider, DbContextOptions<TContext>, TContext> _factory;

        public DynamicDbContextFactory(IServiceProvider serviceProvider, IDbContextFactorySource<TContext> factorySource)
        {
            _serviceProvider = serviceProvider;
            _factory = factorySource.Factory;
        }

        public Task<TContext> CreateDbContextAsync(ConnectionConfiguration connection)
        {
            var builder = new DbContextOptionsBuilder<TContext>();

            builder = builder.UseSqlServer(connection.ConnectionString);
            var db = _factory(_serviceProvider, builder.Options);
            return Task.FromResult(db);
        }
    }
}
