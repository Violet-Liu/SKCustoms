using Repostories.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public static class DbContextBulkOperationExtensions
    {
        public const int DefaultBatchSize = 100000;

        public static void BulkInsert<T>(this DbContext context, IEnumerable<T> entities, int batchSize = DefaultBatchSize)
        {
            var provider = new BulkOperationProvider(context);
            provider.Insert(entities, batchSize);
        }

        public static void MySqlBulkInsert<T>(this DbContext context,IEnumerable<T> entities)
        {
            var provider = new MySqlBulkOperationProvider(context);
            provider.Insert(entities);
        }
    }
}
