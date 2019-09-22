using GraphQL;
using GraphQL.Types;

namespace graphqlpoc.Domain
{
    public class ActivityApiSchema : Schema
    {
        public ActivityApiSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ActivityQuery>();
        }
    }
}