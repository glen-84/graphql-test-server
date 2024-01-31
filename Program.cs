using GraphQLTestServer.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .ModifyOptions(options => options.StripLeadingIFromInterface = true)
    .AddTypes()
    .AddInterfaceType<IUser>()
    .AddObjectType<BookCategory>();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
