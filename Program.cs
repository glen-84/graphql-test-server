using GraphQLTestServer.Types;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication("UserJwtOrCookie")
    .AddCookie()
    .AddJwtBearer("UserJwts")
    .AddPolicyScheme("UserJwtOrCookie", "UserJwtOrCookie", options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            var authorization = context.Request.Headers[HeaderNames.Authorization].FirstOrDefault();

            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
            {
                return "UserJwts";
            }

            return CookieAuthenticationDefaults.AuthenticationScheme;
        };
    });

builder.Services
    .AddHttpContextAccessor();

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .ModifyOptions(options => options.StripLeadingIFromInterface = true)
    .ModifyRequestOptions(
        options => options.IncludeExceptionDetails = builder.Environment.IsDevelopment())
    .AddTypes()
    .AddInterfaceType<IUser>()
    .AddObjectType<BookCategory>();

var app = builder.Build();

app.UseAuthentication();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
