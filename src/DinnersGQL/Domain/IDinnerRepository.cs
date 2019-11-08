using System;

namespace DinnersGQL.Domain
{
    public interface IDinnerRepository
    {
        DinnerModel Add(string description);
        DinnerModel Find(Guid id);
        DinnerModel[] GetAll();
    }
}