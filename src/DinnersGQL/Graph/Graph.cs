using System;
using GraphQL.Conventions;
using System.Reflection;

namespace DinnersGQL.Graph
{
    internal sealed class Mutation
    {
    }

    internal sealed class Query
    {
    }

    internal sealed class UserContext : IUserContext
    {
    }

    internal sealed class Injector : IDependencyInjector
    {
        private readonly IServiceProvider _provider;

        public Injector(IServiceProvider provider)
        {
            _provider = provider;
        }

        public object Resolve(TypeInfo typeInfo) => _provider.GetService(typeInfo);
    }
}
