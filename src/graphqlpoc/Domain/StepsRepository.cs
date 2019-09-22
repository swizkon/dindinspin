using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphqlpoc.Domain
{
    public class StepsRepository
    {
        static IEnumerable<StepsEntry> entries = null;

        public async Task<IEnumerable<StepsEntry>> GetAll()
        {
            return await Task.FromResult(GetQuery().ToList());
        }

        public IQueryable<StepsEntry> GetQuery()
        {
            return GenerateData().AsQueryable();
        }

        private static IEnumerable<StepsEntry> GenerateData()
        {
            if (entries == null)
            {
                var rnd = new Random(Environment.TickCount);
                entries = Enumerable.Range(100, 200).Select(i =>
                {
                    var date = DateTimeOffset.Now.AddHours(-rnd.Next(1, 240));
                    return new StepsEntry
                    {
                        Id = i,
                        UserId = "User-" + (i % 3 + 1),
                        Start = date,
                        End = date.AddMinutes(rnd.Next(30, 120))
                    };
                }
                );
            }
            return entries;
        }
    }
}