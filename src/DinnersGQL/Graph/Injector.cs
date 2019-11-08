using System;
using System.Reflection;
using GraphQL.Conventions;

namespace DinnersGQL.Graph
{
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