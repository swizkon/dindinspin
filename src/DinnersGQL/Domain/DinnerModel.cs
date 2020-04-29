using System;

namespace DinnersGQL.Domain
{
    public sealed class DinnerModel
    {
        public Guid SpinnerId { get; set; }

        public string SpinnerName { get; set; }

        public Guid DinnerId { get; set; }

        public string Description { get; set; }
    }
}