using System;
using System.Collections.Generic;
using System.Linq;
using DinnersGQL.Domain;
using GraphQL.Conventions;

namespace DinnersGQL.Graph
{
    internal sealed class Query
    {
        public IEnumerable<Dinner> Dinners([Inject] IDinnerRepository repository)
            => repository.GetAll().Select(Map);

        public Dinner Dinner([Inject] IDinnerRepository repository, Guid id)
            => Map(repository.Find(id));

        private static Dinner Map(DinnerModel model)
            => model == null ? null : new Dinner
            {
                Id = model.DinnerId,
                Description = model.Description
            };
    }
}