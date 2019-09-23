using System;

namespace graphqlpoc.Domain
{
    public class StepSummary
    {
        public string UserId { get; set; }

        public int StepCount { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}