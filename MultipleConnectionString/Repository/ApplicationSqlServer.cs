using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MultipleConnectionString.Data;
using MultipleConnectionString.Entities;

namespace MultipleConnectionString.Repository
{
    internal class ApplicationSqlServer : IApplication
    {
        private readonly DynamicDbContextFactory<ApplicationContext> _db;
        private ApplicationContext _context;
        
        public ApplicationSqlServer(DynamicDbContextFactory<ApplicationContext> db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IList<Deal>> GetDeals()
        {
            var context = await GetContext().ConfigureAwait(false);
            return await context.Deals.Take(10).ToListAsync();
        }

        private async Task<ApplicationContext> GetContext()
        {
            if (_context == null)
            {
                var conn = new ConnectionConfiguration(
                    "Data Source=DESKTOP-BKVKFLU; Persist Security Info=True; Initial Catalog=koa_staging;Integrated Security=True");
                _context = await _db.CreateDbContextAsync(conn).ConfigureAwait(false);
            }

            return _context;
        }

    }
}
