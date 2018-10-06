using GraphQL.Types;

namespace testGQL.gql
{
    public class HelloWorldSchema : Schema
    {
        public HelloWorldSchema(HelloWorldQuery query)
        {
            base.Query = query;
        }
    }
}