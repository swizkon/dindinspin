using GraphQL;
using GraphQL.Types;

namespace DinDinSpinWeb.Controllers
{

    // https://medium.com/volosoft/building-graphql-apis-with-asp-net-core-419b32a5305b
    public class MyHotelSchema : Schema
    {
        public MyHotelSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<MyHotelQuery>();
        }
    }
}