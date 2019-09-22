using GraphQL.Types;

namespace graphqlpoc.Domain
{
    public class StepsEntryType : ObjectGraphType<StepsEntry>
    {
        public StepsEntryType()
        {
            Field(x => x.Id);
            Field(x => x.UserId);
            Field(x => x.StepCount);
            Field(x => x.Start);
            Field(x => x.End);
        }
    }
}