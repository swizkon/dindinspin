using System;
using System.Collections.Concurrent;
using System.Linq;

namespace DinnersGQL.Domain
{
    internal sealed class DinnerRepository : IDinnerRepository
    {
        private static readonly ConcurrentDictionary<Guid, DinnerModel> _store = new ConcurrentDictionary<Guid, DinnerModel>();

        public DinnerModel Add(string description)
        {
            var dinner = new DinnerModel
            {
                DinnerId = Guid.NewGuid(),
                Description = description
            };
            _store.TryAdd(dinner.DinnerId, dinner);
            return dinner;
        }

        public DinnerModel Find(Guid id)
            => _store.TryGetValue(id, out var dinner) ? dinner : null;

        public DinnerModel[] GetAll()
            => _store.Values.ToArray();
    }
}