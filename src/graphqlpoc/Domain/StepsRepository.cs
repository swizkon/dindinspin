using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace graphqlpoc.Domain
{
    public class StepsRepository
    {
        public static IEnumerable<StepsEntry> entries = null;

        public async Task<IEnumerable<StepsEntry>> GetAll()
        {
            return await Task.FromResult(GetQuery().ToList());
        }

        public IQueryable<StepSummary> GetStepCount(string[] users, DateTime start, DateTime end)
        {
            return GetQuery().Where(x => users.Length == 0 || users.Contains(x.UserId))
                .Where(x => x.Start >= start && x.End <= end)
                .GroupBy(x => x.UserId)
                .Select(g => new StepSummary
                {
                    UserId = g.Key,
                    StepCount = g.Sum(c => c.StepCount),
                    Start = g.Min(c => c.Start).LocalDateTime,
                    End = g.Max(c => c.End).LocalDateTime
                });
        }

        public IQueryable<StepsEntry> GetQuery()
        {
            return GenerateData().AsQueryable();
        }

        private static IEnumerable<StepsEntry> GenerateData()
        {
            if (entries != null)
            {
                return entries;
            }

            var rnd = new Random(Environment.TickCount);
            entries = Enumerable.Range(100, 200).Select(i =>
                {
                    var date = DateTimeOffset.Now.AddHours(-rnd.Next(1, 240));
                    return new StepsEntry
                    {
                        Id = i,
                        UserId = "User-" + (i % 3 + 1),
                        Start = date,
                        End = date.AddMinutes(rnd.Next(30, 120)),
                        StepCount = rnd.Next(10, 500)
                    };
                }
            );
            return entries;
        }
    }
}