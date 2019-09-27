using System;
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

            /*
             * Consolidate multiple requests
             */
            Field<UserSummaryType>("usersummary",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<StringGraphType>
                    {
                        Name = "user"
                    }
                }),
                resolve: context =>
                {
                    var user = context.GetArgument<string>("user") ?? string.Empty;

                    var repo = new Controllers.StepsController();
                    var details = repo.GetUserDetails(user).Value;
                    var steps = repo.GetUserSteps(user).Value;

                    return new UserSummary()
                    {
                        UserId = user,
                        Details = details,
                        Steps = steps.ToArray()
                    };
                }
            );

            /*Version: 2 filtering*/
            Field<ListGraphType<StepSummaryType>>("stepcount",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<StringGraphType>
                    {
                        Name = "users"
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
                    var users = context.GetArgument<string>("users")?.Split(',') ?? Enumerable.Empty<string>();
                    
                    var query = stepsRepository.GetStepCount(users.ToArray(), start: DateTime.Now.AddMonths(-1), end: DateTime.Now);
                    
                    return query.ToList();
                }
            );


            /*Version: 2 filtering*/
            Field<ListGraphType<StepsEntryType>>("stepentries",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IdGraphType>
                    {
                        Name = "userid"
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
                    }

                    var startTime = context.GetArgument<DateTime?>("start");
                    if (startTime.HasValue)
                    {
                        query = query.Where(d => d.Start >= startTime);
                    }

                    var endTime = context.GetArgument<DateTime?>("end");
                    if (endTime.HasValue)
                    {
                        query = query.Where(d => d.End <= endTime);
                    }
                    
                    return query.ToList();
                }
            );
        }
    }
}