using System.Collections.Generic;
using System.Linq;
using GraphQL;
using GraphQL.Types;

namespace graphqlpoc.Domain
{
    public class ActivityQuery : ObjectGraphType
    {
        public ActivityQuery(StepsRepository stepsRepository)
        {
            /*Version: 1 get all*/
            //Field<ListGraphType<ReservationType>>("reservations",
            //    resolve: context => reservationRepository.GetQuery());

            /*Version: 2 filtering*/
            Field<ListGraphType<StepsEntryType>>("stepentries",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IdGraphType>
                    {
                        Name = "userid"
                    },
                    new QueryArgument<IdGraphType>
                    {
                        Name = "id"
                    },
                    new QueryArgument<DateGraphType>
                    {
                        Name = "start"
                    },
                    new QueryArgument<DateGraphType>
                    {
                        Name = "end"
                    }
                }),
                resolve: context =>
                {
                    var query = stepsRepository.GetQuery();

                    var userId = context.GetArgument<string>("userid");
                    if (userId != null)
                    {
                        query = query.Where(d => d.UserId == userId);
                        // return stepsRepository.GetQuery().Where(d => d.UserId == userId);
                    }
                    
                    return query.ToList();
                }
            );
        }
    }
}