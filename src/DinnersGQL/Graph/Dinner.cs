using System;
using GraphQL.Conventions;

namespace DinnersGQL.Graph
{
    [Description("Represents a single Dinner")]
    internal sealed class Dinner
    {
        [Description("The unique dinner identifier")]
        public Guid Id { get; set; }
        [Description("The dinner description")]
        public string Description { get; set; }
    }
}