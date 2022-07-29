using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultipleConnectionString.Data;
using MultipleConnectionString.Repository;

namespace MultipleConnectionString
{
    internal class App
    {
        private IApplication _db;
        public App(IApplication db)
        {
            _db = db;
        }

        public async Task Run()
        {
            var deals = await _db.GetDeals().ConfigureAwait(false);
            foreach (var deal in deals)
            {
                Console.WriteLine(deal.DealName);
            }
            Console.WriteLine("Start");
        }
    }
}
