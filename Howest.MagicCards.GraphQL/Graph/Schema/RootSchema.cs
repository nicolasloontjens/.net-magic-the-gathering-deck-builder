using Howest.MagicCards.GraphQL.Graph.Query;
using GraphQL.Types;

namespace Howest.MagicCards.GraphQL.Graph.RootSchema
{
    public class RootSchema : Schema
    {
        public RootSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQuery>();
        }
    }
}
