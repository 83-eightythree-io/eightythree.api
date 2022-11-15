using System.Text;
using API.Users.GetUser;
using Application;
using Application.Accesses;
using Application.Organizations.CreateOrganization;
using Application.Services.Hashing;
using Application.Services.Mailing;
using Application.Services.Tokenizer;
using Application.Users.DeleteUser;
using Application.Users.GetUser;
using Application.Users.GetUsersList;
using Application.Users.RecoverPassword;
using Application.Users.ResetPassword;
using Application.Users.SignUp;
using Application.Users.UpdatePassword;
using Business.Organizations;
using Business.Users;
using DatabaseByEntityFramework;
using DatabaseByEntityFramework.RefreshTokens;
using DatabaseByEntityFramework.Users;
using HashingBySha1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using QueriesViaDapper.UsersQueries;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddJsonConsole();
}

var databaseConnectionString = builder.Configuration["Database:ConnectionString"];
builder.Services.AddDbContext<Context>(database => database.UseSqlServer(databaseConnectionString));

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(docs =>
{
    docs.Title = "Users API";
    docs.Description = "Users API let you manage users through a RESTFUL API";
    docs.UseRouteNameAsOperationId = true;

    docs.AddSecurity("Bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT", 
        Description = "Type into the input area: {your JWT token}."
    });

    docs.OperationProcessors.Add(
        new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddScoped<IService<SignUpCommand, User>, SignUpService>();
builder.Services.AddScoped<ISignUpRepository, UsersRepository>();

builder.Services.AddScoped<IService<UserAccessCommand, UserAccessResult>, UserAccessService>();
builder.Services.AddScoped<IUserAccessRepository, UsersRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokensRepository>();

builder.Services.AddScoped<IService<RecoverPasswordCommand, string>, RecoverPasswordService>();
builder.Services.AddScoped<IRecoverPasswordRepository, UsersRepository>();

builder.Services.AddScoped<IService<ResetPasswordCommand, bool>, ResetPasswordService>();
builder.Services.AddScoped<IResetPasswordRepository, UsersRepository>();

builder.Services.AddScoped<IService<UpdatePasswordCommand, bool>, UpdatePasswordService>();
builder.Services.AddScoped<IUpdatePasswordRepository, UsersRepository>();

builder.Services.AddScoped<IService<DeleteUserCommand, bool>, DeleteUserService>();
builder.Services.AddScoped<IDeleteUserRepository, UsersRepository>();

builder.Services.AddScoped<IService<CreateOrganizationCommand, Organization>, CreateOrganizationService>();

builder.Services.AddScoped<IQuery<GetUserQuery, UserResult>>(service => 
    new UsersQueriesRepository(databaseConnectionString));

builder.Services.AddScoped<IQuery<GetUsersListQuery, GetUsersListResult>>(service =>
    new UsersQueriesRepository(databaseConnectionString));

builder.Services.AddScoped<IHash, Sha1Hash>();
builder.Services.AddScoped<IEmail>(email =>
    new EmailViaSdk.EmailViaSdk(
        builder.Configuration["Email:Server"],
        int.Parse(builder.Configuration["Email:Port"]),
        bool.Parse(builder.Configuration["Email:Secure"]),
        builder.Configuration["Email:User"],
        builder.Configuration["Email:Password"]
        )
);
builder.Services.AddScoped<ITokenizer>(tokenizer =>
    new TokenGeneratorViaAES.TokenGeneratorViaAES(builder.Configuration["Tokenizer:Key"])
);
builder.Services.AddScoped<ITokenDecoder>(tokenizer =>
    new TokenGeneratorViaAES.TokenGeneratorViaAES(builder.Configuration["Tokenizer:Key"])
);
builder.Services.AddScoped<IAuthorizationAccessToken>(authorization => 
    new AuthorizationAccessTokenViaJwt.AuthorizationAccessTokenViaJwt(
        builder.Configuration["Jwt:Subject"],
        builder.Configuration["Jwt:Key"],
        builder.Configuration["Jwt:Issuer"],
        builder.Configuration["Jwt:Audience"]
        )
);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.Lifetime.ApplicationStarted.Register(() => 
    app.Logger.LogInformation("The application {EnvironmentApplicationName} started", app.Environment.ApplicationName));

app.Run();