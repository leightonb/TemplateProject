using System.Security.Claims;
using TemplateProject.Models;
using TemplateProject.Repositories;
using TemplateProject.Services;
using TemplateProject.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddDbContext<TemplateProjectDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TemplateProjectAPI";
    config.Title = "TemplateProjectAPI v1";
    config.Version = "v1";
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Authorisation-------------------

builder.Services.AddSingleton<TokenService>();
var secretKey = ApiSettings.GenerateSecretByte();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("manager", policy => policy.RequireRole("manager"))
    .AddPolicy("operator", policy => policy.RequireRole("operator"));

// dotnet user-jwts create --scope "greetings_api" --role "admin"
// Authorisation-------------------

// CORS-----------------------------
const string specificOrigins = "AppOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: specificOrigins,
                        policy =>
                        {
                            policy.WithOrigins("http://localhost:5173");
                            policy.WithMethods(["GET", "POST", "PUT", "DELETE"]);
                            policy.WithHeaders(["Content-Type"]);
                        });
});
// CORS-----------------------------

var app = builder.Build();

// CORS-----------------------------
app.UseCors(specificOrigins);
// CORS-----------------------------

// Authorisation-------------------
app.UseAuthentication();
app.UseAuthorization();
// Authorisation-------------------

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TemplateProjectAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapGet("/hello", () => "Hello world!")
  .RequireAuthorization("admin_greetings");

app.MapPost("/register", async (RegisterUser userModel, TokenService service) =>
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<TemplateProjectDbContext>();
    var userRep = new UserRepository(dbContext);
    var userService = new UserService(userRep);

    var user = await userService.Register(userModel);

    if (user is null)
        return Results.NotFound(new { message = "Invalid username or password" });

    var token = service.GenerateToken(user);
    var userDto = new UserDto(user.Id, user.Username, user.FirstName, user.LastName, user.Email);

    return Results.Ok(new { userDto, token });
});

app.MapPost("/login", async (LoginUser userModel, TokenService service) =>
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<TemplateProjectDbContext>();
    var userRep = new UserRepository(dbContext);
    var userService = new UserService(userRep);
    var user = await userService.Authenticate(userModel.Username, userModel.Password);

    if (user is null)
        return Results.NotFound(new { message = "Invalid username or password" });

    var token = service.GenerateToken(user);
    var userDto = new UserDto(user.Id, user.Username, user.FirstName, user.LastName, user.Email);

    return Results.Ok(new { userDto, token });
});

app.MapGet("/operator", (ClaimsPrincipal user) =>
{
    Results.Ok(new { message = $"Authenticated as {user?.Identity?.Name}" });
}).RequireAuthorization("Operator");

app.MapGet("/manager", (ClaimsPrincipal user) =>
{
    Results.Ok(new { message = $"Authenticated as {user?.Identity?.Name}" });
}).RequireAuthorization("Manager");

app.Run();
