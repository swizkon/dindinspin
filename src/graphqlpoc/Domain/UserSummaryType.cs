using GraphQL.Types;

namespace graphqlpoc.Domain
{
    public class UserSummaryType : ObjectGraphType<UserSummary>
    {
        public UserSummaryType()
        {
            Field(x => x.UserId);
            Field(x => x.Details);
            Field(x => x.Steps);
        }
    }
}