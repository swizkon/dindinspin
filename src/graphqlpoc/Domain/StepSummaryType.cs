using GraphQL.Types;

namespace graphqlpoc.Domain
{
    public class StepSummaryType : ObjectGraphType<StepSummary>
    {
        public StepSummaryType()
        {
            Field(x => x.UserId);
            Field(x => x.StepCount);
            Field(x => x.Start);
            Field(x => x.End);
        }
    }
}