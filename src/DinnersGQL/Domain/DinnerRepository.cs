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
            return Put(dinner);
        }

        public DinnerModel Update(DinnerModel dinner)
            => _store.AddOrUpdate(dinner.DinnerId, dinner, (guid, model) => dinner);

        public DinnerModel Find(Guid dinnerId)
            => _store.TryGetValue(dinnerId, out var dinner) ? dinner : null;

        public DinnerModel[] GetAll() 
            => _store.Values.ToArray();

        public DinnerModel Put(DinnerModel dinner) 
            => _store.AddOrUpdate(dinner.DinnerId, dinner, (guid, model) => dinner);
        
        public DinnerModel Get(Guid dinnerId)
            => _store.GetOrAdd(dinnerId, id => new DinnerModel {DinnerId = dinnerId, Description = "Dinner " + dinnerId.ToString()});
    }
}