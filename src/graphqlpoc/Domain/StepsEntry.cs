using System;

namespace graphqlpoc.Domain
{
    public class StepsEntry
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int StepCount { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }
    }
}