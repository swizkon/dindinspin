using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Domain.Models;
using Domain.Repositories;

// using MyHotel.Entities;
// using MyHotel.GraphQL.Types;
// using MyHotel.Repositories;

namespace DinDinSpinWeb.GraphQL
{

    // https://medium.com/volosoft/building-graphql-apis-with-asp-net-core-419b32a5305b
    public class MyHotelSchema : Schema
    {
        public MyHotelSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<DinnerQuery>();
        }
    }

    public class DinnerType : ObjectGraphType<Dinner>
    {
        public DinnerType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.RegisterDate);
        }
    }


    public class DinnerQuery : ObjectGraphType
    {
        /*
         -- Simple test query --

            query TestQuery {
              reservations {
                id
                checkinDate
                checkoutDate
                guest {
                  id
                  name
                  registerDate
                }
                room {
                  id
                  name
                  number
                  allowedSmoking
                  status
                }
              }
            }

        */

        public DinnerQuery(DinnerRepository dinnerRepository)
        {
            /*Version: 1 get all*/
            //Field<ListGraphType<ReservationType>>("reservations",
            //    resolve: context => reservationRepository.GetQuery());

            /*Version: 2 filtering*/
            Field<ListGraphType<DinnerType>>("dinners",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IdGraphType>
                    {
                        Name = "id"
                    },
                    new QueryArgument<DateGraphType>
                    {
                        Name = "checkinDate"
                    },
                    new QueryArgument<DateGraphType>
                    {
                        Name = "checkoutDate"
                    },
                    new QueryArgument<BooleanGraphType>
                    {
                        Name = "roomAllowedSmoking"
                    }
                    /*,
                    new QueryArgument<RoomStatusType>
                    {
                        Name = "roomStatus"
                    }*/
                }),
                resolve: context =>
                {
                    var query = dinnerRepository.GetQuery();

                    var user = (ClaimsPrincipal)context.UserContext;
                    var isUserAuthenticated = ((ClaimsIdentity) user.Identity).IsAuthenticated;

                    var dinnerId = context.GetArgument<int?>("id");
                    if (dinnerId.HasValue)
                    {
                        if (dinnerId.Value <= 0)
                        {
                            context.Errors.Add(new ExecutionError("reservationId must be greater than zero!"));
                            return new List<Dinner>();
                        }

                        return dinnerRepository.GetQuery().Where(d => d.Id == dinnerId.Value);
                    }

                    // var checkinDate = context.GetArgument<DateTime?>("checkinDate");
                    // if (checkinDate.HasValue)
                    // {
                    //     return reservationRepository.GetQuery()
                    //         .Where(r => r.CheckinDate.Date == checkinDate.Value.Date);
                    // }

                    // var allowedSmoking = context.GetArgument<bool?>("roomAllowedSmoking");
                    // if (allowedSmoking.HasValue)
                    // {
                    //     return reservationRepository.GetQuery()
                    //         .Where(r => r.Room.AllowedSmoking == allowedSmoking.Value);
                    // }

                    // var roomStatus = context.GetArgument<RoomStatus?>("roomStatus");
                    // if (roomStatus.HasValue)
                    // {
                    //     return reservationRepository.GetQuery().Where(r => r.Room.Status == roomStatus.Value);
                    // }

                    return query.ToList();
                }
            );
        }
    }
}
