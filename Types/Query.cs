using HotChocolate.Authorization;

namespace GraphQLTestServer.Types;

[QueryType]
public static class Query
{
    [Authorize]
    public static Book GetBook()
    {
        return new Book("C# in depth.", new Author("Jon Skeet"), new BookCategory("Programming"));
    }
}
