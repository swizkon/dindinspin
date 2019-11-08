using System;

namespace DinnersGQL.Domain
{
    public interface IDinnerRepository
    {
        DinnerModel Add(string description);

        DinnerModel Update(DinnerModel dinner);

        DinnerModel Find(Guid dinnerId);

        DinnerModel[] GetAll();

        DinnerModel Put(DinnerModel dinner);

        DinnerModel Get(Guid dinnerId);
    }
}