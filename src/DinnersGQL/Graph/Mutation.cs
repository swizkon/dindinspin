using System;
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

        [Description("Rename a Dinner in the in-memory repository")]
        public Dinner RenameDinner([Inject] IDinnerRepository repository
            , [Description("The dinner id")] Guid dinnerId
            , [Description("The dinner description")] NonNull<string> description)
        {
            var dinner = repository.Get(dinnerId);
            dinner.Description = description;

            var model = repository.Put(dinner);
            return new Dinner
            {
                Id = model.DinnerId,
                Description = model.Description
            };
        }
    }
}