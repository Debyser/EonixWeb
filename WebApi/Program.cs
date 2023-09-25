using ApplicationCore.Services;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Extensions;
using WebApi.Helpers;
//using Microsoft.OpenApi.Models;
//using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureLoggerService();
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.RegisterDbContext(builder.Configuration);
builder.Services.ConfigureRepository();
builder.Services.ConfigureService();

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Register IAppCache as a singleton CachingService
//var config = new MapperConfiguration(cfg =>
//{
//    cfg.AddProfile(new PersonMappingProfile());
//});
//builder.Services.AddSingleton<IMapper>(sp => config.CreateMapper());
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true; // We added the ReturnHttpNotAcceptable = true option, which tells the server that if the client tries to negotiate for the media type the server doesn’t support, it should return the 406 Not Acceptable status code.
}).AddXmlDataContractSerializerFormatters();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = JwtToken.SigningKey,
        ValidateLifetime = true, // to see if it's not invalid
        ClockSkew = TimeSpan.FromMinutes(5) // make sure a tolerance for your time checks
    };
});


builder.Services.AddMvc().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    o.JsonSerializerOptions.AllowTrailingCommas = true;
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    //TODO: error "Self referencing loop detected for property 'contact' with type 'WebApi.Models.ContactView'. Path 'roles[0]'."
    // the solution is the line above but  ask steph si c'est bonne practise
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

var app = builder.Build(); //the Build method builds the WebApplication and registers all the services added with IOC
app.MapHealthChecks("/health");

var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

app.UseSwaggerUI();
app.UseSwagger(x => x.SerializeAsV2 = true);
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
