using System;

namespace DinnersGQL.Domain
{
    public sealed class SpinnerModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}