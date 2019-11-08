using DinnersGQL.Domain;
using GraphQL.Conventions;

namespace DinnersGQL.Graph
{
    internal sealed class Mutation
    {
        [Description("Add a Dinner to the in-memory repository")]
        public Dinner AddDinner([Inject] IDinnerRepository repository, [Description("The dinner description")] NonNull<string> description)
        {
            var model = repository.Add(description);
            return new Dinner
            {
                Id = model.DinnerId,
                Description = model.Description
            };
        }
    }
}