using AutoMapper;
using WebApi.Mappings;
using NLog;
using WebApi.Extensions;
using ApplicationCore.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureLoggerService();
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepository();
builder.Services.ConfigureService();
//var config = new MapperConfiguration(cfg =>
//{
//    cfg.AddProfile(new PersonMappingProfile());
//});
//builder.Services.AddSingleton<IMapper>(sp => config.CreateMapper());
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers(config => {
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true; // We added the ReturnHttpNotAcceptable = true option, which tells the server that if the client tries to negotiate for the media type the server doesn’t support, it should return the 406 Not Acceptable status code.
}).AddXmlDataContractSerializerFormatters();

builder.Services.AddMvc().AddJsonOptions(o => 
{
    o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    o.JsonSerializerOptions.AllowTrailingCommas = true;
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build(); //the Build method builds the WebApplication and registers all the services added with IOC
var logger = app.Services.GetRequiredService<ILoggerService>(); 
app.ConfigureExceptionHandler(logger);

app.UseSwaggerUI();
app.UseSwagger(x => x.SerializeAsV2 = true);
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
